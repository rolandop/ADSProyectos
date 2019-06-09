FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

ENV MongoDB__Host="mongo"
ENV MongoDB__Database="ConfigurationsDb"
ENV MongoDB__User="mongoadmin"
ENV MongoDB__Password="asegsys"

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["ADSConfiguration/ADSConfiguration.csproj", "ADSConfiguration/"]
RUN dotnet restore "ADSConfiguration/ADSConfiguration.csproj"
COPY . .
WORKDIR "/src/ADSConfiguration"
RUN dotnet build "ADSConfiguration.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "ADSConfiguration.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ADSConfiguration.dll"]