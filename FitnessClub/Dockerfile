# ── Этап 1: сборка ───────────────────────────────────────────────────────────
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Копируем .csproj — слой кэшируется
COPY FitnessClub/FitnessClub.csproj ./FitnessClub/
RUN dotnet restore FitnessClub/FitnessClub.csproj

# Копируем весь исходный код
COPY FitnessClub/ ./FitnessClub/

# Публикуем Release-сборку
RUN dotnet publish FitnessClub/FitnessClub.csproj -c Release -o /app/publish

# ── Этап 2: финальный образ (только runtime) ──────────────────────────────────
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

COPY --from=build /app/publish .

ENV ASPNETCORE_ENVIRONMENT=Development
ENV ASPNETCORE_URLS=http://+:8080

EXPOSE 8080

ENTRYPOINT ["dotnet", "FitnessClub.dll"]
