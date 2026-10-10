FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["src/MarcasAutos.API/MarcasAutos.API.csproj", "src/MarcasAutos.API/"]
COPY ["src/MarcasAutos.Application/MarcasAutos.Application.csproj", "src/MarcasAutos.Application/"]
COPY ["src/MarcasAutos.Domain/MarcasAutos.Domain.csproj", "src/MarcasAutos.Domain/"]
COPY ["src/MarcasAutos.Infrastructure/MarcasAutos.Infrastructure.csproj", "src/MarcasAutos.Infrastructure/"]
RUN dotnet restore "src/MarcasAutos.API/MarcasAutos.API.csproj"

COPY . .
WORKDIR "/src/src/MarcasAutos.API"
RUN dotnet build "MarcasAutos.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MarcasAutos.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MarcasAutos.API.dll"]
