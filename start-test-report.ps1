[Console]::OutputEncoding = [System.Text.Encoding]::UTF8

Set-Location -Path (Split-Path -Parent $MyInvocation.MyCommand.Definition)

Write-Host "`Iniciando execução de testes e geração de relatório..." -ForegroundColor Cyan

docker compose up --build --abort-on-container-exit test
if ($LASTEXITCODE -ne 0) {
    Write-Host "Erro ao executar os testes. Verifique o log acima." -ForegroundColor Red
    exit 1
}

docker compose up --build --abort-on-container-exit report
if ($LASTEXITCODE -ne 0) {
    Write-Host "Erro ao gerar o relatório HTML. Verifique o log acima." -ForegroundColor Red
    exit 1
}

docker compose up -d coverage-server
Start-Sleep -Seconds 2

Start-Process "http://localhost:5005"

Write-Host "Relatório disponível em: http://localhost:5005" -ForegroundColor Green
