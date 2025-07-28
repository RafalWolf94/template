# Use the official .NET SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

# Set the working directory
WORKDIR /src

# Copy the .csproj files and restore the dependencies
COPY ["api/Calypso.Api/Calypso.Api.csproj", "Calypso.Api/"]
COPY ["api/Calypso.Presentation/Calypso.Presentation.csproj", "Calypso.Presentation/"]
COPY ["api/Calypso.UseCases/Calypso.UseCases.csproj", "Calypso.UseCases/"]
COPY ["api/Calypso.Infrastructure/Calypso.Infrastructure.csproj", "Calypso.Infrastructure/"]
COPY ["api/Calypso.Domain/Calypso.Domain.csproj", "Calypso.Domain/"]
COPY ["api/Calypso.Adapters.Out/Calypso.Adapters.Out.csproj", "Calypso.Adapters.Out/"]

RUN dotnet restore "Calypso.Api/Calypso.Api.csproj"

# Copy the entire source code
COPY api/ .

# Build the application
RUN dotnet build "Calypso.Api/Calypso.Api.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "Calypso.Api/Calypso.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Final stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Calypso.Api.dll"]