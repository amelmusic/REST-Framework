FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["WebAppTest/WebAppTest.csproj", "WebAppTest/"]
RUN dotnet restore "WebAppTest/WebAppTest.csproj"
COPY . .
WORKDIR "/src/WebAppTest"
RUN dotnet build "WebAppTest.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebAppTest.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebAppTest.dll"]