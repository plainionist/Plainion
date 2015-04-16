@echo off
setlocal

set PATH=%PATH%;C:\Program Files (x86)\MSBuild\12.0\Bin
set PATH=%PATH%;C:\Windows\Microsoft.NET\Framework64\v4.0.30319

msbuild /verbosity:normal packager.msbuild

pause 

endlocal
