FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

COPY domeneshop-ddns.csproj ./
RUN dotnet restore

COPY src/ ./
RUN dotnet publish -c Release -o out



FROM mcr.microsoft.com/dotnet/runtime:8.0
WORKDIR /app

COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "dddns.dll"]
