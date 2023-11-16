FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

COPY dddns.csproj ./
RUN dotnet restore

COPY src/ config.json ./
RUN dotnet publish -c Release -o out



FROM mcr.microsoft.com/dotnet/runtime:7.0
WORKDIR /app

COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "dddns.dll"]
