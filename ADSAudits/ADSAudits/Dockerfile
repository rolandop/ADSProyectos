FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

ENV Global__Services__Configuration__Service="adsconfiguracion"
ENV ServiceId="adsaudits"
ENV ServiceEnvironment="DEV"
ENV ServiceVersion="1.0"

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["ADSAudits/ADSAudits.csproj", "ADSAudits/"]
RUN dotnet restore "ADSAudits/ADSAudits.csproj"
COPY . .
WORKDIR "/src/ADSAudits"
RUN dotnet build "ADSAudits.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "ADSAudits.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ADSAudits.dll"]
#ENV ConnectionString "mongodb://localhost:27017"
#ENV Database Log