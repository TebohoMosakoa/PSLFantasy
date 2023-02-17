#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Backend/APIs/Accounts/Accounts.csproj", "Backend/APIs/Accounts/"]
RUN dotnet restore "Backend/APIs/Accounts/Accounts.csproj"
COPY . .
WORKDIR "/src/Backend/APIs/Accounts"
RUN dotnet build "Accounts.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Accounts.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Accounts.dll"]