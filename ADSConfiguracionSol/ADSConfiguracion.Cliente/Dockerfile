FROM mcr.microsoft.com/dotnet/core/aspnet:2.1-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.1-stretch AS build
WORKDIR /src
COPY ["ADSConfiguracion.Cliente.csproj", "ADSConfiguracion.Cliente/"]
RUN dotnet restore "ADSConfiguracion.Cliente/ADSConfiguracion.Cliente.csproj"
COPY . .
WORKDIR "/src/ADSConfiguracion.Cliente"
RUN dotnet build "ADSConfiguracion.Cliente.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "ADSConfiguracion.Cliente.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ADSConfiguracion.Cliente.dll"]