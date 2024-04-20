# OpenJK_Loader
.Net based loader for OpenJK gameengine

Notes: 
 1) Folder 'OpenJK_Modification_Examples' contains files which allows loader to hook into OpenJK engine. Replace original ones with given.
 2) Change compiler target to shared library instead of executable


Linux deployment: 
 Microsoft doesn't have official support for linux-x86 runtimes, but some folks managed to build it. I've placed working linux-x86 runtime in 'DockerFiles/dotnet6' folder ( Credits: https://github.com/Servarr/dotnet-linux-x86 )
