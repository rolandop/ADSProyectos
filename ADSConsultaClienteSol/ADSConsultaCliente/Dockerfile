FROM mcr.microsoft.com/dotnet/core/aspnet:2.1-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

ENV Global__Services__Databook_Service="databook"

FROM mcr.microsoft.com/dotnet/core/sdk:2.1-stretch AS build
WORKDIR /src
COPY ["ADSConsultaCliente/ADSConsultaCliente.csproj", "ADSConsultaCliente/"]
RUN dotnet restore "ADSConsultaCliente/ADSConsultaCliente.csproj"
COPY . .
WORKDIR "/src/ADSConsultaCliente"
RUN dotnet build "ADSConsultaCliente.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "ADSConsultaCliente.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ADSConsultaCliente.dll"]