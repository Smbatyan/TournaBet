FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["TournaBet.API/TournaBet.API.csproj", "TournaBet.API/"]
RUN dotnet restore "TournaBet.API/TournaBet.API.csproj"
COPY . .
WORKDIR "/src/TournaBet.API"
RUN dotnet build "TournaBet.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TournaBet.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TournaBet.API.dll"]
