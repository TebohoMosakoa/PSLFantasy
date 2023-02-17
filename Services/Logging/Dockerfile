#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/Logging/Logging.csproj", "Services/Logging/"]
RUN dotnet restore "Services/Logging/Logging.csproj"
COPY . .
WORKDIR "/src/Services/Logging"
RUN dotnet build "Logging.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Logging.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Logging.dll"]