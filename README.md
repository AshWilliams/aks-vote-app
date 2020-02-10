---
page_type: sample
languages:
  - C#
products:
  - azure, aks
  - azure-redis-cache
description: "This sample creates a multi-container application in an Azure Kubernetes Service (AKS) cluster."
---

# Azure Voting App

This sample creates a multi-container application in an Azure Kubernetes Service (AKS) cluster. The application interface has been built using .net core / C#. The data component is using Redis.

To walk through a quick deployment of this application, see the AKS [quick start](https://docs.microsoft.com/en-us/azure/aks/kubernetes-walkthrough?WT.mc_id=none-github-nepeters).

To walk through a complete experience where this code is packaged into container images, uploaded to Azure Container Registry, and then run in and AKS cluster, see the [AKS tutorials](https://docs.microsoft.com/en-us/azure/aks/tutorial-kubernetes-prepare-app?WT.mc_id=none-github-nepeters).

# How to build the `docker image`

    git clone https://github.com/AshWilliams/aks-vote-app.git
    cd aks-vote-app\aks-vote-app
    #create the image locally
    docker build -t <login.server>.azurecr.io/aks-vote-app:latest .
    #login to the Azure Container Registry
    docker login --username <acr-username> --password <thepassword> <login.server>.azurecr.io
    #pushing the image to the ACR
    docker push <login.server>.azurecr.io/aks-vote-app:latest


# Running this project on AKS

Login to AKS (From PowerShell o CloudShell)

    az login
    az aks get-credentials --resource-group <rgname> --name <aksname>

Clone the repository

    git clone https://github.com/AshWilliams/aks-vote-app.git
    cd aks-vote-app

Attach the ACR to our AKS

    az aks update -n myAKSCluster -g myResourceGroup --attach-acr <acrName>

Apply the yaml file (deployment and services)

    kubectl apply -f aks-vote-app-redis.yaml

Wait for the `Loadbalancer` service to get the `external ip`

    kubectl get svc azure-vote-front --watch  


# Secrets

Create ACR docker.registry secret

    kubectl create secret docker-registry SECRET-NAME --docker-server=ACRNAME.azurecr.io --docker-username=ACRUSER --docker-password=THEPASSWORD --docker-email=ANYVALIDEMAIL
    
Then in your `yaml`, at the same level as `containers`

    imagePullSecrets:
       - name: SECRET-NAME


# Contexts

    kubectl config get-contexts                          # display list of contexts 
    kubectl config current-context                       # display the current-context
    kubectl config use-context my-cluster-name           # set the default context to my-cluster-name


## Contributing

This project has adopted the [Microsoft Open Source Code of Conduct](https://opensource.microsoft.com/codeofconduct/).
For more information see the [Code of Conduct FAQ](https://opensource.microsoft.com/codeofconduct/faq/) or
contact [opencode@microsoft.com](mailto:opencode@microsoft.com) with any additional questions or comments.
