@minLength(1)
@maxLength(64)
@description('Name of the resource group that will contain all the resources')
param resourceGroupName string = resourceGroup().name

@minLength(1)
@description('Primary location for all resources')
param location string

// @minLength(3)
// @description('Environment for ASP.NET Core. Like "Development", "Production", ..')
// param aspnetcoreEnvironment string = 'Development'

// @description('CPU cores allocated to a single container instance, e.g., 0.5')
// param containerCpuCoreCount string = '0.25'

// @description('Memory allocated to a single container instance, e.g., 1Gi')
// param containerMemory string = '0.5Gi'

var resourceToken = toLower(uniqueString(subscription().id, resourceGroupName, location))

var containerRegistryName = 'acr${resourceToken}'

// common environment variables used by each of the apps
// var env = [
//     {
//         name: 'ASPNETCORE_ENVIRONMENT'
//         value: aspnetcoreEnvironment
//     }
//     {
//         name: 'Logging__Console__FormatterName'
//         value: 'simple'
//     }
//     {
//         name: 'Logging__Console__FormatterOptions__SingleLine'
//         value: 'true'
//     }
//     {
//         name: 'Logging__Console__FormatterOptions__IncludeScopes'
//         value: 'true'
//     }
//     {
//         name: 'ASPNETCORE_LOGGING__CONSOLE__DISABLECOLORS'
//         value: 'true'
//     }
// ]

// log analytics
resource logAnalytics 'Microsoft.OperationalInsights/workspaces@2022-10-01' = {
  name: 'logs${resourceToken}'
  location: location
  properties: {
    retentionInDays: 30
    sku: {
      name: 'PerGB2018'
    }
    workspaceCapping: {
      dailyQuotaGb: 1
    }
  }
}

// the container apps environment
resource containerAppsEnvironment 'Microsoft.App/managedEnvironments@2023-08-01-preview' = {
  name: 'acae${resourceToken}'
  location: location
  properties: {
      appLogsConfiguration: {
          destination: 'log-analytics'
          logAnalyticsConfiguration: {
              customerId: logAnalytics.properties.customerId
              sharedKey: logAnalytics.listKeys().primarySharedKey
          }
      }
  }
}

// the container registry
resource containerRegistry 'Microsoft.ContainerRegistry/registries@2023-11-01-preview' = {
  name: containerRegistryName
  location: location
  sku: {
      name: 'Basic'
  }
  properties: {
      adminUserEnabled: true
      anonymousPullEnabled: false
      dataEndpointEnabled: false
      encryption: {
          status: 'disabled'
      }
      networkRuleBypassOptions: 'AzureServices'
      publicNetworkAccess: 'Enabled'
      zoneRedundancy: 'Disabled'
      metadataSearch: 'Disabled'
  }
}

// identity for the container apps
resource identity 'Microsoft.ManagedIdentity/userAssignedIdentities@2023-07-31-preview' = {
  name: 'acrpullid${resourceToken}'
  location: location
}

var principalId = identity.properties.principalId

// azure system role for setting up acr pull access
var acrPullRole = subscriptionResourceId('Microsoft.Authorization/roleDefinitions', '7f951dda-4ed3-4680-a7ca-43fe172d538d')

// allow acr pulls to the identity used for the aca's
resource aksAcrPull 'Microsoft.Authorization/roleAssignments@2022-04-01' = {
  scope: containerRegistry // Use when specifying a scope that is different than the deployment scope
  name: guid(subscription().id, resourceGroup().id, acrPullRole)
  properties: {
      roleDefinitionId: acrPullRole
      principalType: 'ServicePrincipal'
      principalId: principalId
  }
}

output containerAppsEnvironmentId string = containerAppsEnvironment.id
output containerRegistryId string = containerRegistry.id
output containerRegistryUrl string = containerRegistry.properties.loginServer
output managedIdentityId string = identity.id
output managedIdentityClientId string = identity.properties.clientId
