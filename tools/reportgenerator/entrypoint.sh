#!/bin/sh

echo "Aguardando a geração do arquivo coverage.cobertura.xml..."
while [ ! -f /coverage/coverage.cobertura.xml ]; do
    sleep 1
done

echo "Arquivo encontrado. Gerando relatório HTML..."
reportgenerator \
    -reports:/coverage/coverage.cobertura.xml \
    -targetdir:/coverage/report \
    -reporttypes:Html
