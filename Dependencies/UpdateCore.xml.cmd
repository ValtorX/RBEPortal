@echo off
if exist bin goto exists
mkdir bin
:exists
echo Updating Core
set server=%1
set database=%2
set DBLogin=%3
set DBPassword=%4

set SQLAutentication=User ID=%DBLogin%;Password=%DBPassword%
if "%DBLogin%"=="" set SQLAutentication=Integrated Security=SSPI;

echo Updating Server %server% Database %database% SQLAutentication %SQLAutentication%

%WINDIR%\Microsoft.NET\Framework\v4.0.30319\msbuild.exe UpdateCore.xml /t:Migrate /property:ServerName=%server%;DatabaseName=%database%;SQLAutenticationName="%SQLAutentication%" >bin\UpdateCore.log
 bin\UpdateCore.log
pause
