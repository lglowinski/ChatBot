FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/ChatBot.Categories/ChatBot.Categories.Api/ChatBot.Categories.Api.csproj", "src/ChatBot.Categories/ChatBot.Categories.Api/"]
RUN dotnet restore "src/ChatBot.Categories/ChatBot.Categories.Api/ChatBot.Categories.Api.csproj"
COPY . .
WORKDIR "/src/src/ChatBot.Categories/ChatBot.Categories.Api"
RUN dotnet build "ChatBot.Categories.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "ChatBot.Categories.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ChatBot.Categories.Api.dll"]
