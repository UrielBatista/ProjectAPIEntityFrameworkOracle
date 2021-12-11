@echo off
cd "C:\Users\Uriel\Documents\projects\ProjectAPIEntityFrameworkOracle\Pro.Search.PessoasAPI.UnitTest"
dotnet build
coverlet .\bin\Debug\netcoreapp3.1\Pro.Search.PessoasAPI.UnitTest.dll --target "dotnet" --targetargs "test --no-build"
pause