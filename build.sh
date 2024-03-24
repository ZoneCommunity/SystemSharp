cd System/System.x86
dotnet build
cd bin
dotnet Tools/Mosa.Tool.Launcher.Console.dll -vmware-svga System.x86.dll
cd ../../..