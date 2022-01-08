FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY . .
RUN dotnet restore "Hospital/IntegrationAPI/IntegrationAPI.csproj"

WORKDIR /src
COPY . .
RUN dotnet build "Hospital/IntegrationAPI/IntegrationAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Hospital/IntegrationAPI/IntegrationAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 44317
ENV PORT 44317
CMD ASPNETCORE_URLS=http://*:$PORT dotnet IntegrationAPI.dll
ENTRYPOINT ["dotnet", "IntegrationAPI.dll"]