:: This script must be executed from MSVS Developer Command Prompt

@echo off
nuget restore
msbuild /p:Configuration=Release /p:Platform=x64
rmdir /q /s Release

del WebLibrary-x64.zip
md Release
md Release\locales
md Release\WebLibraryExt
md Release\WebLibraryExt\icons

copy WebLibraryApp\bin\x64\Release\locales\en-US.pak Release\locales\
copy WebLibraryApp\bin\x64\Release\cef.pak Release\
copy WebLibraryApp\bin\x64\Release\cef_100_percent.pak Release\
copy WebLibraryApp\bin\x64\Release\cef_200_percent.pak Release\
copy WebLibraryApp\bin\x64\Release\cef_extensions.pak Release\
copy WebLibraryApp\bin\x64\Release\CefSharp.dll Release\
copy WebLibraryApp\bin\x64\Release\CefSharp.BrowserSubprocess.exe Release\
copy WebLibraryApp\bin\x64\Release\CefSharp.BrowserSubprocess.Core.dll Release\
copy WebLibraryApp\bin\x64\Release\CefSharp.Core.dll Release\
copy WebLibraryApp\bin\x64\Release\CefSharp.WinForms.dll Release\
copy WebLibraryApp\bin\x64\Release\chrome_elf.dll Release\
copy WebLibraryApp\bin\x64\Release\d3dcompiler_47.dll Release\
copy WebLibraryApp\bin\x64\Release\devtools_resources.pak Release\
copy WebLibraryApp\bin\x64\Release\icudtl.dat Release\
copy WebLibraryApp\bin\x64\Release\libcef.dll Release\
copy WebLibraryApp\bin\x64\Release\libEGL.dll Release\
copy WebLibraryApp\bin\x64\Release\libGLESv2.dll Release\
copy WebLibraryApp\bin\x64\Release\natives_blob.bin Release\
copy WebLibraryApp\bin\x64\Release\snapshot_blob.bin Release\
copy WebLibraryApp\bin\x64\Release\v8_context_snapshot.bin Release\

copy WebLibraryApp\bin\x64\Release\WebLibraryApp.exe Release\
copy WebLibraryApp\bin\x64\Release\WebLibraryApp.exe.config Release\

copy WebLibraryDownloader\SingleFile-master.zip Release\
copy WebLibraryDownloader\bin\Release\WebLibraryDownloader.exe Release\
copy WebLibraryDownloader\bin\Release\WebLibraryDownloader.exe.config Release\

copy WebLibraryExt\*.* Release\WebLibraryExt\
copy WebLibraryExt\icons\*.* Release\WebLibraryExt\icons\

copy SetupWebLibrary.bat Release\

powershell.exe -nologo -noprofile -command "& { Add-Type -A 'System.IO.Compression.FileSystem'; [IO.Compression.ZipFile]::CreateFromDirectory('Release', 'WebLibrary-x64.zip'); }"
rmdir /q /s Release
echo WebLibrary-x64.zip file created.