FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["HomeApp.Server/HomeApp.Server.csproj", "HomeApp.Server/"]
COPY ["HomeApp.Persistence/HomeApp.Persistence.csproj", "HomeApp.Persistence/"]
COPY ["HomeApp.Library/HomeApp.Library.csproj", "HomeApp.Library/"]
RUN dotnet restore "HomeApp.Server/HomeApp.Server.csproj"
COPY . .
WORKDIR "/src/HomeApp.Server"
RUN dotnet build "HomeApp.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HomeApp.Server.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HomeApp.Server.dll"]