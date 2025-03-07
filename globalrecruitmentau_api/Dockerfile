# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
# FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src/globalrecruitmentau_api
COPY ["./globalrecruitmentau_api.csproj", "."]
RUN dotnet restore "./globalrecruitmentau_api.csproj"
COPY . .
#WORKDIR "/src/globalrecruitmentau_api"
RUN dotnet build "./globalrecruitmentau_api.csproj" -c $BUILD_CONFIGURATION -o /app/build 

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./globalrecruitmentau_api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Development settings with hot reload
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS development
WORKDIR /app
COPY --from=publish /app/publish . 
ENTRYPOINT ["dotnet", "watch", "run", "--project", "./globalrecruitmentau_api.csproj"]

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "globalrecruitmentau_api.dll"]