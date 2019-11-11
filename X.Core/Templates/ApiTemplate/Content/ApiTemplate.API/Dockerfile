FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["ApiTemplate.API/ApiTemplate.API.csproj", "ApiTemplate.API/"]
RUN dotnet restore "ApiTemplate.API/ApiTemplate.API.csproj"
COPY . .
WORKDIR "/src/ApiTemplate.API"
RUN dotnet build "ApiTemplate.API.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "ApiTemplate.API.csproj" -c Release -o /app
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ApiTemplate.API.dll"]