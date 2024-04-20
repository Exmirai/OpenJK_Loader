#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER app
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["OpenJKLoader.csproj", "."]
RUN dotnet restore "./OpenJKLoader.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "./OpenJKLoader.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./OpenJKLoader.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app

ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=1


USER root

COPY /DockerFiles/dotnet6 /dotnetruntime/

ARG DEBIAN_FRONTEND=noninteractives
RUN DEBIAN_FRONTEND=noninteractive && \
    dpkg --add-architecture i386 && \
    apt-get update && \
    apt-get --assume-yes install libc6:i386 libncurses5:i386 libstdc++6:i386 zlib1g:i386 libcurl4:i386

USER app
COPY --from=publish /app/publish .
ENTRYPOINT ["/dotnetruntime/dotnet", "/app/OpenJKLoader.dll", "+", "set", "fs_homepath", "/app/openjkruntime/", "+", "set", "fs_game", "MBII"]