@echo off
dotnet build src/Skybrud.Essentials.Maps --configuration Debug /t:rebuild /t:pack -p:PackageOutputPath=c:\nuget