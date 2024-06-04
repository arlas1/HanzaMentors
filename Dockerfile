FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# copy csproj and restore as distinct layers
COPY *.sln .

# base
COPY Base.BLL/*.csproj ./Base.BLL/
COPY Base.BLL.Contracts/*.csproj ./Base.BLL.Contracts/
COPY Base.DAL.Contracts/*.csproj ./Base.DAL.Contracts/
COPY Base.DAL.EF/*.csproj ./Base.DAL.EF/
COPY Base.Domain/*.csproj ./Base.Domain/
COPY Base.Domain.Contract/*.csproj ./Base.Domain.Contract/
COPY Base.Helpers/*.csproj ./Base.Helpers/
COPY Base.Public/*.csproj ./Base.Public/
COPY Base.Resources/*.csproj ./Base.Resources/
COPY Base.Tests/*.csproj ./Base.Tests/

# app
COPY App.BLL/*.csproj ./App.BLL/
COPY App.BLL.Contracts/*.csproj ./App.BLL.Contracts/
COPY App.BLL.DTO/*.csproj ./App.BLL.DTO/
COPY App.DAL.Contracts/*.csproj ./App.DAL.Contracts/
COPY App.DAL.EF/*.csproj ./App.DAL.EF/
COPY App.DAL.DTO/*.csproj ./App.DAL.DTO/
COPY App.Domain/*.csproj ./App.Domain/
COPY App.Helpers/*.csproj ./App.Helpers/
COPY App.Public.DTO/*.csproj ./App.Public.DTO/
COPY App.Resources/*.csproj ./App.Resources/
COPY App.Tests/*.csproj ./App.Tests/
COPY App.TestsApi/*.csproj ./App.TestsApi/

# mvc webapp
COPY WebApp/*.csproj ./WebApp/

# api webapp
COPY WebAppApi/*.csproj ./WebAppApi/

RUN dotnet restore


# base
COPY Base.BLL/. ./Base.BLL/
COPY Base.BLL.Contracts/. ./Base.BLL.Contracts/
COPY Base.DAL.Contracts/. ./Base.DAL.Contracts/
COPY Base.DAL.EF/. ./Base.DAL.EF/
COPY Base.Domain/. ./Base.Domain/
COPY Base.Domain.Contract/. ./Base.Domain.Contract/
COPY Base.Helpers/. ./Base.Helpers/
COPY Base.Public/. ./Base.Public/
COPY Base.Resources/. ./Base.Resources/
COPY Base.Tests/. ./Base.Tests/

# app
COPY App.BLL/. ./App.BLL/
COPY App.BLL.Contracts/. ./App.BLL.Contracts/
COPY App.BLL.DTO/. ./App.BLL.DTO/
COPY App.DAL.Contracts/. ./App.DAL.Contracts/
COPY App.DAL.EF/. ./App.DAL.EF/
COPY App.DAL.DTO/. ./App.DAL.DTO/
COPY App.Domain/. ./App.Domain/
COPY App.Helpers/. ./App.Helpers/
COPY App.Public.DTO/. ./App.Public.DTO/
COPY App.Resources/. ./App.Resources/
COPY App.Tests/. ./App.Tests/
COPY App.TestsApi/. ./App.TestsApi/

# mvc webapp
COPY WebApp/. ./WebApp/

# api webapp
COPY WebAppApi/. ./WebAppApi/

WORKDIR /app/WebApp
RUN dotnet publish -c Release -o out

# switch to runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/WebApp/out ./
ENTRYPOINT ["dotnet", "WebApp.dll"]

#FROM --platform=linux/amd64 mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
#EXPOSE 80
#EXPOSE 8080
#WORKDIR /app
#COPY --from=build /app/WebApp/out ./
#ENTRYPOINT ["dotnet", "WebApp.dll"]
