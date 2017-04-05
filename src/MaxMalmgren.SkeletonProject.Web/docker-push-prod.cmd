@echo off

aws ecr get-login --region eu-west-1 > login.cmd | type login.cmd || goto :error
call login.cmd || goto :error
del login.cmd || goto :error
docker push host/maxmalmgren.skeletonproject.web:latest || goto :error

goto :EOF

:error
echo Failed with error # %errorlevel%
exit /b %errorlevel%