cd ZoneOS/ZoneOS.x86
dotnet build
cd bin
dotnet Tools/Mosa.Tool.Launcher.Console.dll -launch-off -autostart -threading -oMax -vmware-svga -include ../../ZoneOS/include ZoneOS.x86.dll
# -launch-off -output-asm -output-debug -output-hash
cd ../../..

qemu-system-x86_64 -m 128M -smp cores=1 -cpu qemu32,+sse4.1,abm,bmi1,bmi2,popcnt -vga vmware -display sdl -drive format=raw,file="/var/folders/6g/cn2k0n6n6mxf063hwd89rbch0000gn/T/MOSA/ZoneOS.x86.img"
# kvm for linux 