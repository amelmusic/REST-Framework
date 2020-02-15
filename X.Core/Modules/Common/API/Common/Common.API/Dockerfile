FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["Common.API/Common.API.csproj", "Common.API/"]
RUN dotnet restore "Common.API/Common.API.csproj"
COPY . .
WORKDIR "/src/Common.API"
RUN dotnet build "Common.API.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Common.API.csproj" -c Release -o /app
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Common.API.dll"]