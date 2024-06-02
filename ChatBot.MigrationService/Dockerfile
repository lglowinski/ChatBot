FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ChatBot.MigrationService/ChatBot.MigrationService.csproj", "ChatBot.MigrationService/"]
COPY ["AspireOrchestrator.ServiceDefaults/AspireOrchestrator.ServiceDefaults.csproj", "AspireOrchestrator.ServiceDefaults/"]
COPY ["src/ChatBot/ChatBot.Infrastructure/ChatBot.Infrastructure.csproj", "src/ChatBot/ChatBot.Infrastructure/"]
COPY ["src/ChatBot/ChatBot.Application/ChatBot.Application.csproj", "src/ChatBot/ChatBot.Application/"]
COPY ["src/ChatBot/ChatBot.Domain/ChatBot.Domain.csproj", "src/ChatBot/ChatBot.Domain/"]
RUN dotnet restore "ChatBot.MigrationService/ChatBot.MigrationService.csproj"
COPY . .
WORKDIR "/src/ChatBot.MigrationService"
RUN dotnet build "ChatBot.MigrationService.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "ChatBot.MigrationService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ChatBot.MigrationService.dll"]
