FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["X.Core.WebAPI/X.Core.WebAPI.csproj", "X.Core.WebAPI/"]
RUN dotnet restore "X.Core.WebAPI/X.Core.WebAPI.csproj"
COPY . .
WORKDIR "/src/X.Core.WebAPI"
RUN dotnet build "X.Core.WebAPI.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "X.Core.WebAPI.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "X.Core.WebAPI.dll"]