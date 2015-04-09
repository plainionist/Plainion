@echo off
setlocal

set build_home=build
set release_home=bin\Release
set pkg_home=pkg

set PATH=%PATH%;C:\Program Files (x86)\MSBuild\12.0\Bin
set PATH=%PATH%;C:\Windows\Microsoft.NET\Framework64\v4.0.30319


cd ..

%build_home%\nuget.exe restore Plainion.sln

msbuild /verbosity:minimal /p:Configuration=Release;Platform="Any CPU" /t:rebuild Plainion.sln

del /q /f /s %pkg_home%

mkdir %pkg_home%\Core\lib\NET45
copy %build_home%\Plainion.Core.nuspec %pkg_home%\Core\
copy %release_home%\Plainion.Core.dll %pkg_home%\Core\lib\NET45\
copy %release_home%\Plainion.Core.pdb %pkg_home%\Core\lib\NET45\
copy %release_home%\Plainion.Core.xml %pkg_home%\Core\lib\NET45\
%build_home%\nuget pack %pkg_home%\Core\Plainion.Core.nuspec -OutputDirectory %pkg_home%\Core\


mkdir %pkg_home%\Windows\lib\NET45
copy %build_home%\Plainion.Windows.nuspec %pkg_home%\Windows\
copy %release_home%\Plainion.Windows.dll %pkg_home%\Windows\lib\NET45\
copy %release_home%\Plainion.Windows.pdb %pkg_home%\Windows\lib\NET45\
copy %release_home%\Plainion.Windows.xml %pkg_home%\Windows\lib\NET45\
%build_home%\nuget pack %pkg_home%\Windows\Plainion.Windows.nuspec -OutputDirectory %pkg_home%\Windows\


mkdir %pkg_home%\Prism\lib\NET45
copy %build_home%\Plainion.Prism.nuspec %pkg_home%\Prism\
copy %release_home%\Plainion.Prism.dll %pkg_home%\Prism\lib\NET45\
copy %release_home%\Plainion.Prism.pdb %pkg_home%\Prism\lib\NET45\
copy %release_home%\Plainion.Prism.xml %pkg_home%\Prism\lib\NET45\
%build_home%\nuget pack %pkg_home%\Prism\Plainion.Prism.nuspec -OutputDirectory %pkg_home%\Prism\



pause

endlocal
