# C# Web Server Template

## Table of Contents

- [About](#about)
- [Getting Started](#getting_started)
- [Usage](#usage)
- [Note](#notes)

## About <a name = "about"></a>

Write about 1-2 paragraphs describing the purpose of your project.

## Getting Started <a name = "getting_started"></a>

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes. See [deployment](#deployment) for notes on how to deploy the project on a live system.

### Prerequisites

C# Environment:

IDE ENV install:
VSCode: 
```https://code.visualstudio.com/docs/languages/csharp```

.Net SDK:
```https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/sdk-7.0.403-windows-x64-installer?journey=vs-code```


Create New Solution

```
dotnet new sln -o template-api-server-sln
```

Create  API/Web Project
```
dotnet new webapi -n template-api-server

dotnet nuget add source --name nuget.org https://api.nuget.org/v3/index.json

dotnet sln add .\template-api-server\template-api-server.csproj

dotnet add package Swashbuckle.AspNetCore.Annotations
```

Create New Tests Project
```
dotnet new xunit -n template-api-server.Tests
dotnet sln add .\template-api-server.Tests\template-api-server.Tests.csproj

dotnet add .\template-api-server.Tests\template-api-server.Tests.csproj reference .\template-api-server\template-api-server.csproj

//Mock Service
dotnet add package Moq

//Utilities to test ASP.NET Core MVC applications.
dotnet add package Microsoft.AspNetCore.Mvc.Testing

dotnet add package Microsoft.AspNetCore.TestHost
```

Create Core/Domain Project
```
dotnet new classlib -n template-api-server.Core
dotnet sln add .\template-api-server.Core\template-api-server.Core.csproj
```

Create Data/Infrastructure Project:
```
dotnet new classlib -n template-api-server.Data
dotnet sln add .\template-api-server.Data\template-api-server.Data.csproj
```

Create Service/Application Project:
```
dotnet new classlib -n template-api-server.Services
dotnet sln add .\template-api-server.Services\template-api-server.Services.csproj
```

Add references from the API project to the Core, Data, and Services 
```
dotnet add .\template-api-server\template-api-server.csproj reference .\template-api-server.Core\template-api-server.Core.csproj
dotnet add .\template-api-server\template-api-server.csproj reference .\template-api-server.Data\template-api-server.Data.csproj
dotnet add .\template-api-server\template-api-server.csproj reference .\template-api-server.Services\template-api-server.Services.csproj
```


Give examples


### Installing

A step by step series of examples that tell you how to get a development env running.

Say what the step will be

```
Give the example
```

And repeat

```
until finished
```

End with an example of getting some data out of the system or using it for a little demo.

## Usage <a name = "usage"></a>

Add notes about how to use the system.


Run

Dev
```
export ASPNETCORE_ENVIRONMENT=Development / set ASPNETCORE_ENVIRONMENT=Development

dotnet run --launch-profile https
```



### Notes  <a name = "notes"></a>

```
API/Web Project: This project contains the API controllers, middleware, and other web-specific configurations. It's the entry point for external requests.

Core/Domain Project: This project contains the domain entities, value objects, and domain logic. It should have no dependencies on other projects and should be persistence-agnostic.

Data/Infrastructure Project: This project contains the Entity Framework context, configurations, migrations, and repositories. It references the Core/Domain project for the entities and provides data access to the API/Web project.

Service/Application Project: This project contains the application logic, DTOs (Data Transfer Objects), and services that the API controllers use. It often sits between the API and the Data projects, acting as a mediator.

Tests Project: Contains unit tests, integration tests, etc., for the above projects.
```


DI:

```
Transient:
A new instance of the service is created each time it is requested.
Suitable for lightweight, stateless services.
```

```
Scoped:
A new instance of the service is created once per request (i.e., per HTTP request in a web application). It's shared within that single request scope.
Suitable for services that need to maintain state within a single request.
```


```
Singleton:
A single instance of the service is created and shared across all requests and uses. This single instance exists for the lifetime of the application.
Suitable for services that need to maintain state across multiple requests and for the entire lifetime of the application.
```
