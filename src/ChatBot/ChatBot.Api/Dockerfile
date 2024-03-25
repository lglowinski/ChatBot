FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/ChatBot/ChatBot.Api/ChatBot.Api.csproj", "src/ChatBot/ChatBot.Api/"]
COPY ["AspireOrchestrator/AspireOrchestrator.ServiceDefaults/AspireOrchestrator.ServiceDefaults.csproj", "AspireOrchestrator/AspireOrchestrator.ServiceDefaults/"]
COPY ["src/ChatBot/ChatBot.OpenAiFacade/ChatBot.OpenAiFacade.csproj", "src/ChatBot/ChatBot.OpenAiFacade/"]
COPY ["src/ChatBot/ChatBot.Domain/ChatBot.Domain.csproj", "src/ChatBot/ChatBot.Domain/"]
COPY ["src/ChatBot/ChatBot.Application/ChatBot.Application.csproj", "src/ChatBot/ChatBot.Application/"]
COPY ["src/ChatBot/ChatBot.Contracts/ChatBot.Contracts.csproj", "src/ChatBot/ChatBot.Contracts/"]
COPY ["src/ChatBot/ChatBot.Infrastructure/ChatBot.Infrastructure.csproj", "src/ChatBot/ChatBot.Infrastructure/"]
RUN dotnet restore "src/ChatBot/ChatBot.Api/ChatBot.Api.csproj"
COPY . .
WORKDIR "/src/src/ChatBot/ChatBot.Api"
RUN dotnet build "ChatBot.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "ChatBot.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ChatBot.Api.dll"]
