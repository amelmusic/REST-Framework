FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["PermissionModule.API/PermissionModule.API.csproj", "PermissionModule.API/"]
RUN dotnet restore "PermissionModule.API/PermissionModule.API.csproj"
COPY . .
WORKDIR "/src/PermissionModule.API"
RUN dotnet build "PermissionModule.API.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "PermissionModule.API.csproj" -c Release -o /app
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PermissionModule.API.dll"]