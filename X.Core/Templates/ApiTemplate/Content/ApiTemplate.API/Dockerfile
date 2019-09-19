FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
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
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ApiTemplate.API.dll"]