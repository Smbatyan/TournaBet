FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
# WORKDIR /src

# Copy project files
COPY ["src/Tournabet.Host/Tournabet.Host.csproj", "src/Tournabet.Host/"]
COPY ["src/TournaBet.Auth/TournaBet.Auth.csproj", "src/TournaBet.Auth/"]
COPY ["src/Tournabet.Auth.Shared/Tournabet.Auth.Shared.csproj", "src/Tournabet.Auth.Shared/"]
COPY ["src/TournaBet.Shared/TournaBet.Shared.csproj", "src/TournaBet.Shared/"]
COPY ["src/TournaBet.Feed/TournaBet.Feed.csproj", "src/TournaBet.Feed/"]
COPY ["src/TournaBet.Liga/TournaBet.Liga.csproj", "src/TournaBet.Liga/"]

# Restore packages
RUN dotnet restore "src/Tournabet.Host/Tournabet.Host.csproj"

# Copy the rest of the application
COPY . .

# Build the project
WORKDIR "/src/Tournabet.Host"
RUN dotnet build "Tournabet.Host.csproj" -c Release -o /app/build
