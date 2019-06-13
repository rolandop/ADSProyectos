FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80

ENV TZ=America/Guayaquil
ENV Global__Services__kafka__Service="kafka:9092"
ENV Global__Services__Configuration__Service="adsconfiguration"
ENV Service="adsdatabook"
ENV ServiceId="adsdatabook"
ENV ServiceEnvironment="PRD"
ENV ServiceVersion="1.0"


FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["ADSDataBook/ADSDataBook.csproj", "ADSDataBook/"]
RUN dotnet restore "ADSDataBook/ADSDataBook.csproj"
COPY . .
WORKDIR "/src/ADSDataBook"
RUN dotnet build "ADSDataBook.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "ADSDataBook.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ADSDataBook.dll"]