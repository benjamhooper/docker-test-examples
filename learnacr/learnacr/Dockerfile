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