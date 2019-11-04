FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
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
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "PermissionModule.API.dll"]