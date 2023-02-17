#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Backend/Services/Upload/Upload.csproj", "Backend/Services/Upload/"]
RUN dotnet restore "Backend/Services/Upload/Upload.csproj"
COPY . .
WORKDIR "/src/Backend/Services/Upload"
RUN dotnet build "Upload.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Upload.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Upload.dll"]