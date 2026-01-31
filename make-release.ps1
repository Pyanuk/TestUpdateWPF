# Скрипт для автоматического создания релиза

param(
    [Parameter(Mandatory=$true)]
    [string]$Version
)

Write-Host "Creating release for version $Version..." -ForegroundColor Green

# Обновляем версию в .csproj
$csprojPath = "TestAutoUpdate\TestAutoUpdate.csproj"
$csprojContent = Get-Content $csprojPath -Raw
$csprojContent = $csprojContent -replace '<Version>[\d\.]+</Version>', "<Version>$Version</Version>"
$csprojContent = $csprojContent -replace '<AssemblyVersion>[\d\.]+</AssemblyVersion>', "<AssemblyVersion>$Version.0</AssemblyVersion>"
$csprojContent = $csprojContent -replace '<FileVersion>[\d\.]+</FileVersion>', "<FileVersion>$Version.0</FileVersion>"
$csprojContent | Set-Content $csprojPath -NoNewline

Write-Host "Version updated in .csproj" -ForegroundColor Green

# Коммитим изменения
git add .
git commit -m "Bump version to $Version"
git push

Write-Host "Changes committed and pushed" -ForegroundColor Green

# Создаем и пушим тег
$tag = "v$Version"
git tag $tag
git push origin $tag

Write-Host "Tag $tag created and pushed" -ForegroundColor Green
Write-Host ""
Write-Host "GitHub Actions will now automatically:" -ForegroundColor Cyan
Write-Host "  1. Build the application" -ForegroundColor Cyan
Write-Host "  2. Create Squirrel packages" -ForegroundColor Cyan
Write-Host "  3. Create GitHub Release with all files" -ForegroundColor Cyan
Write-Host ""
Write-Host "Check progress at: https://github.com/Pyanuk/TestUpdateWPF/actions" -ForegroundColor Yellow
