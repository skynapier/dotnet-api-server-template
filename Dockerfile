# Use the official image as a parent image
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app


# Use the SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Copy csproj and restore as distinct layers
COPY *.sln .
COPY template-api-server/*.csproj ./template-api-server/
COPY template-api-server.Core/*.csproj ./template-api-server.Core/
COPY template-api-server.Data/*.csproj ./template-api-server.Data/
COPY template-api-server.Services/*.csproj ./template-api-server.Services/
COPY template-api-server.Tests/*.csproj ./template-api-server.Tests/

RUN dotnet restore

# Copy everything else and build app
COPY template-api-server/. ./template-api-server/
COPY template-api-server.Core/. ./template-api-server.Core/
COPY template-api-server.Data/. ./template-api-server.Data/
COPY template-api-server.Services/. ./template-api-server.Services/
WORKDIR "/src/template-api-server"
RUN dotnet build "template-api-server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "template-api-server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "template-api-server.dll"]
