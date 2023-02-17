#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Backend/APIs/Teams/Teams.csproj", "Backend/APIs/Teams/"]
RUN dotnet restore "Backend/APIs/Teams/Teams.csproj"
COPY . .
WORKDIR "/src/Backend/APIs/Teams"
RUN dotnet build "Teams.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Teams.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Teams.dll"]