@echo off
cd "C:\Users\uriel_batista\Documents\Projetos_GitHub\ProjectAPIEntityFrameworkOracle\Pro.Search.PessoasAPI.UnitTest"
dotnet build
coverlet .\bin\Debug\netcoreapp3.1\Pro.Search.PessoasAPI.UnitTest.dll --target "dotnet" --targetargs "test --no-build"
pause