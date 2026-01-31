# Build and create NuGet package for Squirrel

$projectPath = "TestAutoUpdate\TestAutoUpdate.csproj"
$publishDir = ".\publish"
$releasesDir = ".\Releases"

# Clean
if (Test-Path $publishDir) { Remove-Item $publishDir -Recurse -Force }
if (Test-Path $releasesDir) { Remove-Item $releasesDir -Recurse -Force }
New-Item -ItemType Directory -Path $releasesDir -Force | Out-Null

Write-Host "Building project..." -ForegroundColor Green
dotnet publish $projectPath -c Release -r win-x64 --self-contained true -o $publishDir

Write-Host "Creating NuGet package..." -ForegroundColor Green

# Create nuspec file
$nuspecContent = @"
<?xml version="1.0" encoding="utf-8"?>
<package xmlns="http://schemas.microsoft.com/packaging/2010/07/nuspec.xsd">
  <metadata>
    <id>TestAutoUpdate</id>
    <version>1.0.0</version>
    <authors>Pyanuk</authors>
    <description>Test Auto Update Application</description>
  </metadata>
  <files>
    <file src="publish\**" target="lib\net8.0-windows\" />
  </files>
</package>
"@

$nuspecContent | Out-File -FilePath "TestAutoUpdate.nuspec" -Encoding UTF8

# Create NuGet package
nuget pack TestAutoUpdate.nuspec -OutputDirectory $releasesDir

Write-Host "Done! Package created in $releasesDir" -ForegroundColor Green
Write-Host "Now run: squirrel releasify --package=$releasesDir\TestAutoUpdate.1.0.0.nupkg --releaseDir=$releasesDir" -ForegroundColor Cyan
