# Use the official .NET 8 SDK image for building
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj and restore dependencies
COPY ["DigiStore.WebService.csproj", "./"]
RUN dotnet restore "DigiStore.WebService.csproj"

# Copy everything else and build
COPY . .
RUN dotnet build "DigiStore.WebService.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "DigiStore.WebService.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Use the official .NET 8 runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Copy published files
COPY --from=publish /app/publish .

# Set environment to production
ENV ASPNETCORE_ENVIRONMENT=Production

ENTRYPOINT ["dotnet", "DigiStore.WebService.dll"]
