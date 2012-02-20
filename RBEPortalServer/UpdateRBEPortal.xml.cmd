@echo off
if exist bin goto exists
mkdir bin
:exists
echo Updating RBEPortal
set server=%1
set database=%2
set DBLogin=%3
set DBPassword=%4

set SQLAutentication=User ID=%DBLogin%;Password=%DBPassword%
if "%DBLogin%" == "" set SQLAutentication=Integrated Security=SSPI;

if "%DBLogin%" == "" goto nologin
%WINDIR%\Microsoft.NET\Framework\v4.0.30319\aspnet_regsql.exe -S %server% -U %DBLogin% -P %DBPassword% -A all -d %database%
goto done
:nologin
%WINDIR%\Microsoft.NET\Framework\v4.0.30319\aspnet_regsql.exe -E -S %server% -A all -d %database%
:done

echo Updating Server %server% Database %database% SQLAutentication %SQLAutentication%

%WINDIR%\Microsoft.NET\Framework\v4.0.30319\msbuild.exe UpdateRBEPortal.xml /t:Migrate /property:ServerName=%server%;DatabaseName=%database%;SQLAutenticationName="%SQLAutentication%" >bin\UpdateRBEPortal.log
 bin\UpdateRBEPortal.log

pause
