@minLength(1)
@maxLength(64)
@description('Name of the resource group that will contain all the resources')
param resourceGroupName string = resourceGroup().name

@minLength(1)
@description('Primary location for all resources')
param location string = resourceGroup().location

@minLength(3)
@description('Environment for ASP.NET Core. Like "Development", "Production", ..')
param aspnetcoreEnvironment string

param containerRegistryUrl string
param managedIdentityClientId string
param managedIdentityId string

param apiserviceContainerImage string
param webfrontendContainerImage string

var resourceToken = toLower(uniqueString(subscription().id, resourceGroupName, location))

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

resource containerAppsEnvironment 'Microsoft.App/managedEnvironments@2023-05-01' = {
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

module apiservice 'containerapp.bicep' = {
  name: 'apiservicemodule'
  params: {
    location: location
    appName: 'apiservice'
    aspnetcoreEnvironment: aspnetcoreEnvironment
    containerAppsEnvironmentId: containerAppsEnvironment.id
    containerImage: apiserviceContainerImage
    containerRegistryUrl: containerRegistryUrl
    managedIdentityClientId: managedIdentityClientId
    managedIdentityId: managedIdentityId
  }
}

module webfrontend 'containerapp.bicep' = {
  name: 'webfrontendmodule'
  params: {
    location: location
    appName: 'webfrontend'
    aspnetcoreEnvironment: aspnetcoreEnvironment
    containerAppsEnvironmentId: containerAppsEnvironment.id
    containerImage: webfrontendContainerImage
    containerRegistryUrl: containerRegistryUrl
    managedIdentityClientId: managedIdentityClientId
    managedIdentityId: managedIdentityId
  }
}
