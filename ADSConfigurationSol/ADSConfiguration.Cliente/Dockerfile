FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

ENV TZ=America/Guayaquil
ENV Global__Services__Configuration__Service="adsconfiguration"
ENV Service="adsconfigurationcliente"
ENV ServiceId="adsconfigurationcliente"
ENV ServiceEnvironment="DEV"
ENV ServiceVersion="1.0"

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["ADSConfiguration.Cliente/ADSConfiguration.Cliente.csproj", "ADSConfiguration.Cliente/"]
RUN dotnet restore "ADSConfiguration.Cliente/ADSConfiguration.Cliente.csproj"
COPY . .
WORKDIR "/src/ADSConfiguration.Cliente"
RUN dotnet build "ADSConfiguration.Cliente.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "ADSConfiguration.Cliente.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ADSConfiguration.Cliente.dll"]