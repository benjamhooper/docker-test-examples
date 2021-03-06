# Docker Examples

1. Counter
    
    - Basically just counts +1 every second in a container

    ```docker
    FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
    COPY bin/Release/netcoreapp3.1/publish/ App/
    WORKDIR /App
    ENTRYPOINT ["dotnet", "NetCore.Docker.dll"]
    ```

2. Learn ACR (Azure Container Registries)

    - 2 Classes Name and Greet
    - Can be build in ACR without locally having Docker installed

    ```docker
    FROM mcr.microsoft.com/dotnet/core/runtime:3.1-buster-slim AS base
    WORKDIR /app

    FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
    WORKDIR /src
    COPY ["learnacr.csproj", ""]
    COPY ["../Greet/Greet.csproj", "../Greet/"]
    RUN dotnet restore "./learnacr.csproj"
    COPY . .
    WORKDIR "/src/."
    RUN dotnet build "learnacr.csproj" -c Release -o /app/build

    FROM build AS publish
    RUN dotnet publish "learnacr.csproj" -c Release -o /app/publish

    FROM base AS final
    WORKDIR /app
    COPY --from=publish /app/publish .
    ENTRYPOINT ["dotnet", "learnacr.dll"]
    ```

    - Running this command to build it in your ACR on Azure:

        `az acr build -t learnacr -r benhooper -f .\learnacr\Dockerfile .`
    ```
    Parameters for the command above
    "az acr" : Target Azure, Azure Container Registry
    "build" : Build container
    "-t acr" : Target project
    "-r ben hooper" : Container Registry within Azure
    "-f .\learnacr\Dockerfile ." : location of dockerfile, put files in container
    ```
3. ASP.NET Core MVC
    - Build Project: `dotnet new mvc -o aspnetapp`
    - Add Dockerfile

    ```dockerfile
    FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
    WORKDIR /app

    # Copy csproj and restore as distinct layers
    COPY *.csproj ./
    RUN dotnet restore

    # Copy everything else and build
    COPY . ./
    RUN dotnet publish -c Release -o out

    # Build runtime image
    FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
    WORKDIR /app
    COPY --from=build-env /app/out .
    ENTRYPOINT ["dotnet", "aspnetapp.dll"]
    ```
    - Build Docker `docker build -t aspnetapp .`
    - Run Docker `docker run -d -p 8080:80 --name myapp aspnetapp` on port 8080
    
    Note: To stop; type `docker ps`, Get ID from container running. Then type`docker stop {id}`.