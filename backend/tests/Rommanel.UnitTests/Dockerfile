FROM mcr.microsoft.com/dotnet/sdk:8.0

WORKDIR /app

RUN dotnet tool install --global coverlet.console
ENV PATH="${PATH}:/root/.dotnet/tools"

COPY src/Rommanel.Application/*.csproj ./src/Rommanel.Application/
COPY src/Rommanel.Domain/*.csproj ./src/Rommanel.Domain/
COPY tests/Rommanel.UnitTests/*.csproj ./tests/Rommanel.UnitTests/

RUN dotnet restore ./tests/Rommanel.UnitTests/Rommanel.UnitTests.csproj

COPY src ./src
COPY tests ./tests

WORKDIR /app/tests/Rommanel.UnitTests
RUN dotnet build

CMD coverlet ./bin/Debug/net8.0/Rommanel.UnitTests.dll \
    --target "dotnet" \
    --targetargs "test --no-build" \
    --format cobertura \
    --output /app/coverage/coverage.cobertura.xml
