FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the project file and restore dependencies
COPY ["SolarWatch/SolarWatch.csproj", "/src"]
RUN dotnet restore "/src/SolarWatch.csproj"

# Copy the entire application code
COPY . .
WORKDIR "/src/SolarWatch"
RUN dotnet build "SolarWatch.csproj" -c Release -o /app/build

FROM base AS final
WORKDIR /app
COPY . .
ENTRYPOINT ["dotnet", "SolarWatch.dll"]