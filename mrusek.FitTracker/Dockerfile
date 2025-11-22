# .devcontainer/Dockerfile
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:9.0-alpine AS build
ARG TARGETARCH
WORKDIR /src

# Copy solution
COPY *.sln .

# Copy project files (matching real folder names)
COPY src/mrusek.FitTracker.Api/*.csproj src/mrusek.FitTracker.Api/
COPY src/mrusek.FitTracker.Application/*.csproj src/mrusek.FitTracker.Application/
COPY src/mrusek.FitTracker.Domain/*.csproj src/mrusek.FitTracker.Domain/
COPY src/mrusek.FitTracker.Infrastructure/*.csproj src/mrusek.FitTracker.Infrastructure/

# Restore dependencies
RUN dotnet restore src/mrusek.FitTracker.Api/mrusek.FitTracker.Api.csproj

# Copy all source
COPY src/. ./src/

# Build
RUN dotnet build src/mrusek.FitTracker.Api/mrusek.FitTracker.Api.csproj -c Release -o /app/build

# Publish
FROM build AS publish
RUN dotnet publish src/mrusek.FitTracker.Api/mrusek.FitTracker.Api.csproj -c Release -o /app/publish /p:UseAppHost=false

# Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0-alpine
RUN apk add --no-cache icu-libs tzdata

# Ustawienia dla globalizacji .NET
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
ENV TZ=Europe/Warsaw
ENV ASPNETCORE_HTTP_PORTS=5000
RUN addgroup -S appgroup && adduser -S appuser -G appgroup
USER appuser
EXPOSE 5000
WORKDIR /app
COPY --from=publish /app/publish .
USER $APP_UID
ENTRYPOINT ["dotnet", "mrusek.FitTracker.Api.dll"]
