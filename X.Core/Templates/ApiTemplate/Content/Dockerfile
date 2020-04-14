FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 5000
ENV ASPNETCORE_URLS=http://+:5000

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY . .
RUN dotnet tool install codegenx --add-source ./CorePackages --global
ENV PATH="${PATH}:/root/.dotnet/tools"

FROM build AS publish
RUN dotnet publish "ApiTemplate.API" -c Release -o /app
FROM base AS final
WORKDIR /app
COPY --from=publish /app .

ENTRYPOINT ["dotnet", "ApiTemplate.API.dll"]