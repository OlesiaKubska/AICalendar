# STAGE 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY AICalendar/*.csproj ./AICalendar/
WORKDIR /app/AICalendar
RUN dotnet restore

COPY AICalendar/. ./
RUN dotnet publish -c Release -o out

# STAGE 2: Run
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/AICalendar/out ./
ENTRYPOINT ["dotnet", "AICalendar.dll"]