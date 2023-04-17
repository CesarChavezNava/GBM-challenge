FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 5024

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY Broker.sln ./
COPY Shared/Domain/Broker.Core/*.csproj ./Shared/Domain/Broker.Core/
COPY Accounts/Domain/Broker.Accounts.Domain/*.csproj ./Accounts/Domain/Broker.Accounts.Domain/
COPY Accounts/Domain/Broker.Accounts.Domain.Tests/*.csproj ./Accounts/Domain/Broker.Accounts.Domain.Tests/
COPY Accounts/Application/Broker.Accounts.Application/*.csproj ./Accounts/Application/Broker.Accounts.Application/
COPY Accounts/Infrastructure/Broker.Accounts.Infrastructure.SQL/*.csproj ./Accounts/Infrastructure/Broker.Accounts.Infrastructure.SQL/
COPY Accounts/Infrastructure/Broker.Accounts.Infrastructure.API/*.csproj ./Accounts/Infrastructure/Broker.Accounts.Infrastructure.API/

RUN dotnet restore
COPY . .
WORKDIR /src/Shared/Domain/Broker.Core
RUN dotnet build -c Release -o /app/build

WORKDIR /src/Accounts/Domain/Broker.Accounts.Domain
RUN dotnet build -c Release -o /app/build

WORKDIR /src/Accounts/Application/Broker.Accounts.Application
RUN dotnet build -c Release -o /app/build

WORKDIR /src/Accounts/Infrastructure/Broker.Accounts.Infrastructure.SQL
RUN dotnet build -c Release -o /app/build

WORKDIR /src/Accounts/Infrastructure/Broker.Accounts.Infrastructure.API
RUN dotnet build -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT [ "dotnet", "Broker.Accounts.Infrastructure.API.dll" ]