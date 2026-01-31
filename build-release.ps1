# Скрипт для создания Squirrel релиза

# Параметры
$projectPath = "TestAutoUpdate\TestAutoUpdate.csproj"
$outputDir = ".\Releases"
$appName = "TestAutoUpdate"

# Очистка и сборка
Write-Host "Сборка проекта..." -ForegroundColor Green
dotnet publish $projectPath -c Release -r win-x64 --self-contained false -o ".\publish"

# Создание Squirrel релиза
Write-Host "Создание Squirrel пакета..." -ForegroundColor Green

# Установка Squirrel CLI если не установлен
if (-not (Get-Command "squirrel" -ErrorAction SilentlyContinue)) {
    Write-Host "Установка Squirrel CLI..." -ForegroundColor Yellow
    dotnet tool install -g Clowd.Squirrel.CommandLine
}

# Создание релиза
squirrel pack --packId $appName --packVersion 1.0.0 --packDirectory ".\publish" --releaseDir $outputDir

Write-Host "Готово! Релиз создан в папке $outputDir" -ForegroundColor Green
Write-Host "Загрузи файлы из $outputDir в GitHub Releases" -ForegroundColor Cyan
