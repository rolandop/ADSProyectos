FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

ENV Global__Services__kafka__Service="kafka:9092"
ENV Global__Services__Configuration__Service="adsconfiguration"
ENV Service="adsconsultapla"
ENV ServiceId="adsconsultapla"
ENV ServiceEnvironment="PRD"
ENV ServiceVersion="1.0"


FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["ADSConsultaPla/ADSConsultaPla.csproj", "ADSConsultaPla/"]
RUN dotnet restore "ADSConsultaPla/ADSConsultaPla.csproj"
COPY . .
WORKDIR "/src/ADSConsultaPla"
RUN dotnet build "ADSConsultaPla.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "ADSConsultaPla.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ADSConsultaPla.dll"]