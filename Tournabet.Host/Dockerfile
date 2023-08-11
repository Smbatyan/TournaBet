FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Tournabet.Host/Tournabet.Host.csproj", "Tournabet.Host/"]
COPY ["TournaBet.Auth/TournaBet.Auth.csproj", "TournaBet.Auth/"]
COPY ["Tournabet.Auth.Shared/Tournabet.Auth.Shared.csproj", "Tournabet.Auth.Shared/"]
COPY ["TournaBet.Shared/TournaBet.Shared.csproj", "TournaBet.Shared/"]
RUN dotnet restore "Tournabet.Host/Tournabet.Host.csproj"
COPY . .
WORKDIR "/src/Tournabet.Host"
RUN dotnet build "Tournabet.Host.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Tournabet.Host.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Tournabet.Host.dll"]
