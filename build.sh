cd System/System.x86
dotnet build
cd bin
dotnet Tools/Mosa.Tool.Launcher.Console.dll -oMax -launch-off System.x86.dll
cd ../../..

cp /var/folders/6g/cn2k0n6n6mxf063hwd89rbch0000gn/T/MOSA/System.x86.img ISO/CD_root/images

mv ISO/CD_root/images/System.x86.img ISO/CD_root/images/System.img

mkisofs -o ISO/System.iso -b isolinux/isolinux.bin -c isolinux/boot.cat -no-emul-boot -boot-load-size 4 -boot-info-table ISO/CD_root

perl ISO/isohybrid.pl ISO/System.iso

qemu-system-x86_64 -cdrom ISO/System.iso