FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80

ENV TZ=CET

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