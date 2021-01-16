#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:5.0-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["ScanFolderWatchdog.Worker/ScanFolderWatchdog.Worker.csproj", "ScanFolderWatchdog.Worker/"]
COPY ["ScanFolderWatchdog.Common/ScanFolderWatchdog.Common.csproj", "ScanFolderWatchdog.Common/"]
RUN dotnet restore "ScanFolderWatchdog.Worker/ScanFolderWatchdog.Worker.csproj"
COPY . .
WORKDIR "/src/ScanFolderWatchdog.Worker"
RUN dotnet build "ScanFolderWatchdog.Worker.csproj" -c Release -o /app/build



FROM build AS publish
RUN dotnet publish "ScanFolderWatchdog.Worker.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
RUN mkdir /myvol
VOLUME /scans
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ScanFolderWatchdog.Worker.dll"]