@echo off

del /Q /S wwwroot\static\gen || goto :error
del /Q /S obj || goto :error
del /Q /S bin || goto :error

call npm run build-prod

dotnet restore
dotnet publish -o obj\Docker\publish -c release || goto :error
docker-compose -f ..\..\docker-compose.yml -f ..\..\docker-compose.override.yml -f ..\..\docker-compose.vs.release.yml -p dockercompose2732615719 build || goto :error
docker tag maxmalmgren.skeletonproject.web:latest 740264218619.dkr.ecr.eu-west-1.amazonaws.com/maxmalmgren.skeletonproject.web:latest || goto :error

goto :EOF

:error
echo Failed with error # %errorlevel%
exit /b %errorlevel%