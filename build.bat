cd System\System.x86
dotnet build
cd bin
dotnet Tools/Mosa.Tool.Launcher.Console.dll -launch-off -autostart -threading -oMax -vmware-svga -include ../../System/include System.x86.dll

cd ..\..\..

qemu-system-x86_64 -accel whpx -m 128M -smp cores=1 -cpu qemu32,+sse4.1,abm,bmi1,bmi2,popcnt -vga vmware -display sdl -drive format=raw,file="%USERPROFILE%\AppData\Local\Temp\MOSA\System.x86.img"
