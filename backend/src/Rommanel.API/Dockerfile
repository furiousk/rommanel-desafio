FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY src/Rommanel.API/*.csproj src/Rommanel.API/
COPY src/Rommanel.Application/*.csproj src/Rommanel.Application/
COPY src/Rommanel.Domain/*.csproj src/Rommanel.Domain/
COPY src/Rommanel.Infrastructure/*.csproj src/Rommanel.Infrastructure/

RUN dotnet restore src/Rommanel.API/Rommanel.API.csproj

COPY . .

WORKDIR /src/src/Rommanel.API
RUN dotnet build -c Release -o /app/build
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Rommanel.API.dll"]
