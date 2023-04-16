#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Account/Infrastructure/Broker.Accounts.API/Broker.Accounts.API.csproj", "Accounts/Infrastructure/Broker.Accounts.API/"]
RUN dotnet restore "Account/Infrastructure/Broker.Accounts.API/Broker.Accounts.API.csproj"
COPY . .
WORKDIR "/src/Account/Infrastructure/Broker.Accounts.API"
RUN dotnet build "Broker.Accounts.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Broker.Accounts.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Broker.Accounts.API.dll"]