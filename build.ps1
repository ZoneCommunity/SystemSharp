cd System\System.x86
dotnet build
cd bin
dotnet Tools/Mosa.Tool.Launcher.Console.dll -launch-off -autostart -threading -oMax -vmware-svga -vmware -vmdk -include ../../System/include System.x86.dll

cd ..\..\..

Start-Process "C:\Program Files (x86)\VMware\VMware Workstation\vmware.exe" -ArgumentList '-x', '"C:\Users\Creeper\Documents\Virtual Machines\Other 64-bit (3)\Other 64-bit (3).vmx"' -NoNewWindow -Wait