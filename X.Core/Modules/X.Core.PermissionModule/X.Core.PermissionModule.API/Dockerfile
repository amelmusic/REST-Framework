FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["X.Core.PermissionModule.API/X.Core.PermissionModule.API.csproj", "X.Core.PermissionModule.API/"]
RUN dotnet restore "X.Core.PermissionModule.API/X.Core.PermissionModule.API.csproj"
COPY . .
WORKDIR "/src/X.Core.PermissionModule.API"
RUN dotnet build "X.Core.PermissionModule.API.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "X.Core.PermissionModule.API.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "X.Core.PermissionModule.API.dll"]