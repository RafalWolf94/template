FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

WORKDIR /src

COPY ["api/Web.Api/Web.Api.csproj", "Web.Api/"]
COPY ["api/Web.Presentation/Web.Presentation.csproj", "Web.Presentation/"]
COPY ["api/Web.UseCases/Web.UseCases.csproj", "Web.UseCases/"]
COPY ["api/Web.Infrastructure/Web.Infrastructure.csproj", "Web.Infrastructure/"]
COPY ["api/Web.Domain/Web.Domain.csproj", "Web.Domain/"]
COPY ["api/Web.Adapters.Out/Web.Adapters.Out.csproj", "Web.Adapters.Out/"]

RUN dotnet restore "Web.Api/Web.Api.csproj"

COPY api/ .

RUN dotnet build "Web.Api/Web.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Web.Api/Web.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Web.Api.dll"]
