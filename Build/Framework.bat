@echo off


setlocal
set msbuild=%SystemRoot%\Microsoft.NET\Framework\v4.0.30319\MSBuild.exe
set flags=/nologo /p:Configuration=Release /p:Optimize=true /p:DebugSymbols=false


pushd ..

call %msbuild% %flags% Framework\Nine.sln

echo.
echo.
echo Registering Assembly
echo Processor Architecture: %PROCESSOR_ARCHITECTURE%

if %PROCESSOR_ARCHITECTURE% == x86 goto regx86
if %PROCESSOR_ARCHITECTURE% == AMD64 goto regx64

:regx86
reg add "HKLM\SOFTWARE\Microsoft\.NETFramework\v4.0.30319\AssemblyFoldersEx\Engine Nine" /f /ve /d "%CD%\References\x86"
reg add "HKLM\SOFTWARE\Microsoft\XNA\AssemblyFolders\v4.0\Xbox 360\AssemblyFoldersEx\Engine Nine" /f /ve /d "%CD%\References\Xbox 360"
reg add "HKLM\SOFTWARE\Microsoft\XNA\AssemblyFolders\v4.0\Windows Phone\AssemblyFoldersEx\Engine Nine" /f /ve /d "%CD%\References\Windows Phone"
goto endreg

:regx64
reg add "HKLM\SOFTWARE\Wow6432Node\Microsoft\.NETFramework\v4.0.30319\AssemblyFoldersEx\Engine Nine" /f /ve /d "%CD%\References\x86"
reg add "HKLM\SOFTWARE\Wow6432Node\Microsoft\XNA\AssemblyFolders\v4.0\Xbox 360\AssemblyFoldersEx\Engine Nine" /f /ve /d "%CD%\References\Xbox 360"
reg add "HKLM\SOFTWARE\Wow6432Node\Microsoft\XNA\AssemblyFolders\v4.0\Windows Phone\AssemblyFoldersEx\Engine Nine" /f /ve /d "%CD%\References\Windows Phone"
goto endreg

:endreg

popd

endlocal