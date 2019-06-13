FROM mcr.microsoft.com/dotnet/core/runtime:2.2-stretch-slim AS base
WORKDIR /app

ENV Global__Services__kafka__Service="kafka:9092"
ENV CONSUMER__NAME="adsloggerconsumer"
ENV MongoDB__Host="mongo"
ENV MongoDB__Database="LogsDb"
ENV MongoDB__User="mongoadmin"
ENV MongoDB__Password="asegsys"

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["ADSAuditsConsumer/ADSAuditsConsumer.csproj", "ADSAuditsConsumer/"]
RUN dotnet restore "ADSAuditsConsumer/ADSAuditsConsumer.csproj"
COPY . .
WORKDIR "/src/ADSAuditsConsumer"
RUN dotnet build "ADSAuditsConsumer.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "ADSAuditsConsumer.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ADSAuditsConsumer.dll"]