FROM mcr.microsoft.com/dotnet/sdk:8.0

WORKDIR /app

RUN dotnet tool install --global dotnet-reportgenerator-globaltool
ENV PATH="${PATH}:/root/.dotnet/tools"

COPY entrypoint.sh /app/entrypoint.sh
RUN chmod +x /app/entrypoint.sh

ENTRYPOINT ["/app/entrypoint.sh"]
