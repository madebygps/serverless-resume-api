https://learn.microsoft.com/en-us/azure/azure-functions/functions-infrastructure-as-code?tabs=bicep

# Provision required resources

1. Create a [resource group](https://learn.microsoft.com/azure/azure-resource-manager/management/manage-resource-groups-portal#what-is-a-resource-group)
    ``` sh
    az group create --name <resource-group-name> --location <your-region>
    ```

2. Create a deployment
    ```sh
    az deployment group create --resource-group <resource-group-name> --template-file <path-to-bicep>
    ```

3. In your newly created storage account, create a [blob container](https://learn.microsoft.com/azure/storage/blobs/storage-quickstart-blobs-cli) called `resume`

    ```sh
    az storage container create --account-name <storage-account-name> --name resume
    ```

 4. Upload `myresume.json` to that newly created blob container. 
    ```sh
    az storage blob upload --account-name <storage-account-name> --container-name resume --name myresume.json --file myresume.json 
    ```