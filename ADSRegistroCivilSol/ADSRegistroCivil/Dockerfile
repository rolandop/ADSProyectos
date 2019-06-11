FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80

ENV TZ=CET

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["ADSRegistroCivil/ADSRegistroCivil.csproj", "ADSRegistroCivil/"]
RUN dotnet restore "ADSRegistroCivil/ADSRegistroCivil.csproj"
COPY . .
WORKDIR "/src/ADSRegistroCivil"
RUN dotnet build "ADSRegistroCivil.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "ADSRegistroCivil.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ADSRegistroCivil.dll"]