:: This script must be executed from MSVS Developer Command Prompt

@echo off
msbuild /p:Configuration=Release /p:Platform=x64 /t:restore
msbuild /p:Configuration=Release /p:Platform=x64

rmdir /q /s Release
del WebLibrary-x64.zip
md Release
md Release\locales

copy bin\x64\Release\locales\en-US.pak Release\locales\
copy bin\x64\Release\cef.pak Release\
copy bin\x64\Release\cef_100_percent.pak Release\
copy bin\x64\Release\cef_200_percent.pak Release\
copy bin\x64\Release\cef_extensions.pak Release\
copy bin\x64\Release\CefSharp.dll Release\
copy bin\x64\Release\CefSharp.BrowserSubprocess.exe Release\
copy bin\x64\Release\CefSharp.BrowserSubprocess.Core.dll Release\
copy bin\x64\Release\CefSharp.Core.dll Release\
copy bin\x64\Release\CefSharp.WinForms.dll Release\
copy bin\x64\Release\chrome_elf.dll Release\
copy bin\x64\Release\d3dcompiler_47.dll Release\
copy bin\x64\Release\devtools_resources.pak Release\
copy bin\x64\Release\icudtl.dat Release\
copy bin\x64\Release\libcef.dll Release\
copy bin\x64\Release\libEGL.dll Release\
copy bin\x64\Release\libGLESv2.dll Release\
copy bin\x64\Release\natives_blob.bin Release\
copy bin\x64\Release\snapshot_blob.bin Release\
copy bin\x64\Release\v8_context_snapshot.bin Release\

copy bin\x64\Release\WebLibrary.exe Release\
copy bin\x64\Release\WebLibrary.exe.config Release\

powershell.exe -nologo -noprofile -command "& { Add-Type -A 'System.IO.Compression.FileSystem'; [IO.Compression.ZipFile]::CreateFromDirectory('Release', 'WebLibrary-x64.zip'); }"
rmdir /q /s Release
echo WebLibrary-x64.zip file created.