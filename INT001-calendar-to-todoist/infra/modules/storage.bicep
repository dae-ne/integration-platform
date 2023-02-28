param storageAccountName string
param location string

resource storageAccount 'Microsoft.Storage/storageAccounts@2022-09-01' = {
  name: storageAccountName
  location: location
  sku: {
    name: 'Standard_LRS'
  }
  kind: 'StorageV2'
  properties: {
    minimumTlsVersion: 'TLS1_2'
    allowBlobPublicAccess: false
  }
}

resource blobService 'Microsoft.Storage/storageAccounts/blobServices@2022-09-01' = {
  name: 'default'
  parent: storageAccount
  properties: {

  }
}

resource blobServiceContainer 'Microsoft.Storage/storageAccounts/blobServices/containers@2022-09-01' = {
  name: 'webjobs-host'
  parent: blobService
  properties: {
    publicAccess: 'None'
    denyEncryptionScopeOverride: false
    immutableStorageWithVersioning: {
      enabled: false
    }
    defaultEncryptionScope: '$account-encryption-key'
  }
}

resource tableService 'Microsoft.Storage/storageAccounts/tableServices@2022-09-01' = {
  name: 'default'
  parent: storageAccount
  properties: {
    
  }
}

resource authTable 'Microsoft.Storage/storageAccounts/tableServices/tables@2022-09-01' = {
  name: 'auth'
  parent: tableService
  properties: {
    signedIdentifiers: [
      {
        accessPolicy: {
          permission: 'r'
        }
        id: 'auth1'
      }
    ]
  }
}
