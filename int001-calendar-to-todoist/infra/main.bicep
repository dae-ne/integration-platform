param hostingPlanName string
param storageAccountName string
param functionAppName string
param location string = resourceGroup().location

module hostingPlanModule 'modules/hostingPlan.bicep' = {
  name: 'HostingPlanDeploy'
  params: {
    hostingPlanName: hostingPlanName
    location: location
  }
}

module storageAccountModule 'modules/storage.bicep' = {
  name: 'StorageAccountDeploy'
  params: {
    storageAccountName: storageAccountName
    location: location
  }
}

module functionAppModule 'modules/functionApp.bicep' = {
  name: 'FunctionAppDeploy'
  params: {
    functionAppName: functionAppName
    hostingPlanName: hostingPlanName
    storageAccountName: storageAccountName
    location: location
  }
  dependsOn:[
    hostingPlanModule
    storageAccountModule
  ]
}
