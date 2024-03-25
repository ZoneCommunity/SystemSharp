using System;
using Mosa.Kernel.BareMetal;
using Mosa.Runtime.Plug;
using Mosa.DeviceDriver.ISA;
using Mosa.DeviceSystem.Fonts;
using Mosa.DeviceSystem.Services;

using System.Collections.Generic;
using System.Drawing;

using ZoneOS.Driver;

namespace ZoneOS;

public static class Program
{
	[Plug("Mosa.Runtime.StartUp::BootOptions")]
	public static void SetBootOptions()
	{
		BootSettings.EnableDebugOutput = true;
		//BootSettings.EnableVirtualMemory = true;
		//BootSettings.EnableMinimalBoot = true;
	}
	public static DeviceService DeviceService { get; private set; }

	public static PCService PCService { get; private set; }

	public static Random Random { get; private set; }

	// public static Taskbar Taskbar { get; private set; }

	public static void EntryPoint()
	{
		Debug.WriteLine("Program::EntryPoint()");

		DeviceService = Mosa.Kernel.BareMetal.Kernel.ServiceManager.GetFirstService<DeviceService>();
		PCService = Mosa.Kernel.BareMetal.Kernel.ServiceManager.GetFirstService<PCService>();
		Random = new Random();

		Console.ResetColor();
		Console.Clear();

		// Display.DefaultFont = Utils.Load(File.ReadAllBytes("font.bin"));
		Display.DefaultFont = new ASC16Font();

		// if (!Display.Initialize()) HAL.Abort("An error occurred when initializing the graphics driver.");
		if (!Display.Initialize()) Console.WriteLine("Failed to load graphics driver.");

		Utils.Mouse = DeviceService.GetFirstDevice<StandardMouse>().DeviceDriver as StandardMouse;

		// if (Utils.Mouse == null) HAL.Abort("Mouse not found.");
		if (Utils.Mouse == null) Console.WriteLine("Mouse not found.");

		Utils.BackColor = Color.Indigo;
		Utils.Mouse.SetScreenResolution(Display.Width, Display.Height);

		Mouse.Initialize();	

		for (; ; )
				{
					// Clear screen
					Display.Clear(Color.FromArgb(22, 169, 253));

					Mouse.Draw();
					Display.Update();
				}
			}
}
