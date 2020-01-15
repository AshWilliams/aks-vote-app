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

# Running this project on AKS

Login to AKS (From PowerShell o CloudShell)

    az login
    az aks get-credentials --resource-group <rgname> --name <aksname>

Clone the repository

    git clone https://github.com/AshWilliams/aks-vote-app.git
    cd aks-vote-app

Apply the yaml file (deployment and services)

    kubectl apply -f aks-vote-app-redis.yaml

Wait for the `Loadbalancer` service to get the `external ip`

    kubectl get svc azure-vote-front --watch  


## Contributing

This project has adopted the [Microsoft Open Source Code of Conduct](https://opensource.microsoft.com/codeofconduct/).
For more information see the [Code of Conduct FAQ](https://opensource.microsoft.com/codeofconduct/faq/) or
contact [opencode@microsoft.com](mailto:opencode@microsoft.com) with any additional questions or comments.
