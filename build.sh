cd System/System.x86
dotnet build
cd bin
dotnet Tools/Mosa.Tool.Launcher.Console.dll -launch-off -autostart -threading -oMax -vmware-svga -include ../../System/include System.x86.dll
# -launch-off -output-asm -output-debug -output-hash
cd ../../..

# ISO Creation -- Temporary disabled for building speed/or until I can get Limine to build MOSA as an ISO (which someone removed because it was 'useless')
# cp /var/folders/6g/cn2k0n6n6mxf063hwd89rbch0000gn/T/MOSA/System.x86.img ISO/CD_root/images
# mv ISO/CD_root/images/System.x86.img ISO/CD_root/images/System.img
# mkisofs -o ISO/System.iso -b isolinux/isolinux.bin -c isolinux/boot.cat -no-emul-boot -boot-load-size 4 -boot-info-table ISO/CD_root
# perl ISO/isohybrid.pl ISO/System.iso
# qemu-system-x86_64 -cdrom ISO/System.iso

qemu-system-x86_64 -accel hvf -m 128M -smp cores=1 -cpu qemu32,+sse4.1,abm,bmi1,bmi2,popcnt -vga vmware -display sdl -drive format=raw,file="/var/folders/6g/cn2k0n6n6mxf063hwd89rbch0000gn/T/MOSA/System.x86.img"
# kvm for linux