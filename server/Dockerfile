FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Build and publish both projects
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy solution and project files first for optimized restore
COPY ["PM-API.sln", "."]
COPY ["PM-API/PM-API.csproj", "PM-API/"]
COPY ["PM-API.Data/PM-API.Data.csproj", "PM-API.Data/"]

RUN dotnet restore "PM-API.sln"

# Copy the rest of the source
COPY . .

# Build
RUN dotnet build "PM-API/PM-API.csproj" -c $BUILD_CONFIGURATION -o /app/build/PM-API \
 && dotnet build "PM-API.Data/PM-API.Data.csproj" -c $BUILD_CONFIGURATION -o /app/build/PM-API.Data

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "PM-API/PM-API.csproj" -c $BUILD_CONFIGURATION -o /app/publish/PM-API /p:UseAppHost=false \
 && dotnet publish "PM-API.Data/PM-API.Data.csproj" -c $BUILD_CONFIGURATION -o /app/publish/PM-API.Data /p:UseAppHost=false

FROM base AS final
WORKDIR /app
# Copy web API publish output as the main app
COPY --from=publish /app/publish/PM-API .
# Also include the Data project artifacts (not used as entrypoint by default)
RUN mkdir -p /app/aux/PM-API.Data
COPY --from=publish /app/publish/PM-API.Data /app/aux/PM-API.Data

ENTRYPOINT ["dotnet", "PM-API.dll"]
