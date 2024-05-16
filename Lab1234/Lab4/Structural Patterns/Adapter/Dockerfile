FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Structural Patterns/Structural Patterns.csproj", "Structural Patterns/"]
RUN dotnet restore "Structural Patterns/Structural Patterns.csproj"
COPY . .
WORKDIR "/src/Structural Patterns"
RUN dotnet build "Structural Patterns.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Structural Patterns.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Structural Patterns.dll"]
