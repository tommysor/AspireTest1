param location string = resourceGroup().location

param managedIdentityId string
param managedIdentityClientId string

param containerAppsEnvironmentId string

param containerRegistryUrl string

param containerImage string

resource app 'Microsoft.App/containerApps@2023-05-01' = {
  name: 'webfrontend'
  location: location
  identity: {
    type: 'UserAssigned'
    userAssignedIdentities: {
      '${managedIdentityId}': {}
    }
  }
  properties: {
    environmentId: containerAppsEnvironmentId
    configuration: {
      activeRevisionsMode: 'Single'
      ingress: {
        external: true
        targetPort: 8080
        transport: 'http'
        allowInsecure: false
      }
      registries: [
        {
          server: containerRegistryUrl
          identity: managedIdentityId
        }
      ]
    }
    template: {
      containers: [
        {
          image: containerImage
          name: 'webfrontend'
          env: [
            {
              name: 'AZURE_CLIENT_ID'
              value: managedIdentityClientId
            }
            {
              name: 'OTEL_DOTNET_EXPERIMENTAL_OTLP_EMIT_EVENT_LOG_ATTRIBUTES'
              value: 'true'
            }
            {
              name: 'OTEL_DOTNET_EXPERIMENTAL_OTLP_EMIT_EXCEPTION_LOG_ATTRIBUTES'
              value: 'true'
            }
          ]
          resources: {
            cpu: json('0.25')
            memory: '0.5Gi'
          }
          probes: [
            {
              type: 'Startup'
              tcpSocket: {
                port: 8080
              }
              timeoutSeconds: 3
              periodSeconds: 1
              initialDelaySeconds: 3
              successThreshold: 1
              failureThreshold: 30
            }
            {
              type: 'Readiness'
              httpGet: {
                port: 8080
                path: '/health'
                scheme: 'HTTP'
              }
              timeoutSeconds: 5
              periodSeconds: 5
              initialDelaySeconds: 10
              successThreshold: 1
              failureThreshold: 10
            }
            {
              type: 'Liveness'
              httpGet: {
                port: 8080
                path: '/alive'
                scheme: 'HTTP'
              }
              timeoutSeconds: 2
              periodSeconds: 10
              initialDelaySeconds: 5
              successThreshold: 1
              failureThreshold: 3
            }
          ]
        }
      ]
      scale: {
        minReplicas: 1
        maxReplicas: 1
      }
    }
  }
}
