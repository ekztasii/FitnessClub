# ── Этап 1: сборка ───────────────────────────────────────────────────────────
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Копируем .csproj отдельно — если зависимости не менялись,
# этот слой берётся из кэша и повторная сборка быстрее
COPY FitnessClub.csproj ./
RUN dotnet restore FitnessClub.csproj

# Копируем весь исходный код
COPY . ./

# Публикуем Release-сборку
RUN dotnet publish FitnessClub.csproj -c Release -o /app/publish

# ── Этап 2: финальный образ (только runtime, без SDK) ────────────────────────
# aspnet весит ~200 МБ вместо ~800 МБ у sdk
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

COPY --from=build /app/publish .

ENV ASPNETCORE_ENVIRONMENT=Development
ENV ASPNETCORE_URLS=http://+:8080

EXPOSE 8080

ENTRYPOINT ["dotnet", "FitnessClub.dll"]
