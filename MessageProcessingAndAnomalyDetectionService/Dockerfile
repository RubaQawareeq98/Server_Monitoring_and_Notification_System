FROM mcr.microsoft.com/dotnet/runtime:9.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["MessageProcessingAndAnomalyDetectionService/MessageProcessingAndAnomalyDetectionService.csproj", "MessageProcessingAndAnomalyDetectionService/"]
COPY ["Domain/Domain.csproj", "Domain/"]
RUN dotnet restore "MessageProcessingAndAnomalyDetectionService/MessageProcessingAndAnomalyDetectionService.csproj"
COPY . .
WORKDIR "/src/MessageProcessingAndAnomalyDetectionService"
RUN dotnet build "MessageProcessingAndAnomalyDetectionService.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "MessageProcessingAndAnomalyDetectionService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MessageProcessingAndAnomalyDetectionService.dll"]
