FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8082
EXPOSE 8083

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/ChatBot.Users/ChatBot.Users.csproj", "src/ChatBot.Users/"]
RUN dotnet restore "src/ChatBot.Users/ChatBot.Users.csproj"
COPY . .
WORKDIR "/src/src/ChatBot.Users"
RUN dotnet build "ChatBot.Users.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "ChatBot.Users.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ChatBot.Users.dll"]
