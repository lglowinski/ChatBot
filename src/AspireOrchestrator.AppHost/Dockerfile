FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/AspireOrchestrator.AppHost/AspireOrchestrator.AppHost.csproj", "src/AspireOrchestrator.AppHost/"]
COPY ["src/ChatBot.MigrationService/ChatBot.MigrationService.csproj", "src/ChatBot.MigrationService/"]
COPY ["src/ChatBot/ChatBot.Domain/ChatBot.Domain.csproj", "src/ChatBot/ChatBot.Domain/"]
COPY ["src/ChatBot/ChatBot.Application/ChatBot.Application.csproj", "src/ChatBot/ChatBot.Application/"]
COPY ["src/AspireOrchestrator.ServiceDefaults/AspireOrchestrator.ServiceDefaults.csproj", "src/AspireOrchestrator.ServiceDefaults/"]
COPY ["src/ChatBot/ChatBot.Infrastructure/ChatBot.Infrastructure.csproj", "src/ChatBot/ChatBot.Infrastructure/"]
COPY ["src/ChatBot/ChatBot.Api/ChatBot.Api.csproj", "src/ChatBot/ChatBot.Api/"]
COPY ["src/ChatBot/ChatBot.OpenAiFacade/ChatBot.OpenAiFacade.csproj", "src/ChatBot/ChatBot.OpenAiFacade/"]
COPY ["src/ChatBot/ChatBot.Contracts/ChatBot.Contracts.csproj", "src/ChatBot/ChatBot.Contracts/"]
RUN dotnet restore "src/AspireOrchestrator.AppHost/AspireOrchestrator.AppHost.csproj"
COPY . .
WORKDIR "/src/src/AspireOrchestrator.AppHost"
RUN dotnet build "AspireOrchestrator.AppHost.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "AspireOrchestrator.AppHost.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AspireOrchestrator.AppHost.dll"]
