# Your Resume API on Azure Serverless



## Provision required resources

1. Create a [resource group](https://learn.microsoft.com/azure/azure-resource-manager/management/manage-resource-groups-portal#what-is-a-resource-group)
    ``` sh
    az group create --name <resource-group-name> --location <your-region>
    ```

2. Create a deployment
    ```sh
    az deployment sub create --template-file <>.\infra\main.bicep> -l <your-region>   
    ```

3. In your newly created storage account, create a [blob container](https://learn.microsoft.com/azure/storage/blobs/storage-quickstart-blobs-cli) called `resume`

    > **Note**
    > To find your storage account name, you can use:
    > `az storage account list --resource-group <resource-group> --query "[].{Name:name}"`
    ```sh
    az storage container create --account-name <storage-account-name> --name resume
    ```

 4. Upload `myresume.json` to that newly created blob container. 
    ```sh
    az storage blob upload --account-name <storage-account-name> --container-name resume --name myresume.json --file myresume.json 
    ```
5. In your `local.settings.json` add the Storage Account Connection String to the `AzureWebJobsStorage` value.

6. You can now run and debug your Function in your environment

## Manually Deploy Function to Azure from VS Code

I prefer to use DevOps practices to deploy functions (GitHub Actions) instead of manually deploying each time. Here are instructions in case you do want to use VS Code.

1. `Ctrl + Shift + P` and search for Run Task
2. Select `build (Functions)`
3. `Ctrl + Shift + P` and search for Run Task
4. Select `publish (Functions)`
5. Right click on publish in src > bin > Release > net6.0 and select Deploy to Function App
6. Select the subscription you've been working in.
7. Select the Function App you recently provisioned in step 2

    > Note:
    > To find your Function App name, use:
    `az functionapp list --resource-group <resource-group> --query "[].{Name:name}"`

12. View output tab to get Function URL.
13. Resume should be displayed there.

## Configure CI/CD with GitHub actions

1. We'll need to get our Function's Publish Profile, run:
    ```sh
    az functionapp deployment list-publishing-profiles --name {function-name} --resource-group {resource-group} --xml
    ```
2. In your GitHub repo, create a secret named `AZURE_FUNCTIONAPP_PUBLISH_PROFILE` with the contents of your publish profile.
3. Head to Actions tab on your Repo and manually run the workflow. 
