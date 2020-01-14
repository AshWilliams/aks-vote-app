FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine AS build
WORKDIR /src
COPY ["aks-vote-app.csproj", ""]
RUN dotnet restore "aks-vote-app.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "aks-vote-app.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "aks-vote-app.csproj" -c Release -o /app

FROM base AS final
ENV REDIS=localhost:6379

WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "aks-vote-app.dll"]