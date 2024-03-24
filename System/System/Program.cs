// Copyright (c) MOSA Project. Licensed under the New BSD License.

using Mosa.DeviceSystem.HardwareAbstraction;
using Mosa.DeviceSystem.Services;
using Mosa.Kernel.BareMetal;
using Mosa.Runtime.Plug;
using Mosa.DeviceDriver.ISA;
using Mosa.DeviceSystem.Fonts;

using System.Collections.Generic;
using System.Drawing;
using System.IO;

using System.Components;
using System.Driver;
using System.WM;
using System.Apps;


namespace System;

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

	public static Taskbar Taskbar { get; private set; }

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

		//Utils.Fonts = new List<ISimpleFont>
		//{
		//	Display.DefaultFont,
		//	Utils.Load(File.ReadAllBytes("font.bin"))
		//};

		// if (!Display.Initialize()) HAL.Abort("An error occurred when initializing the graphics driver.");
		if (!Display.Initialize()) Console.WriteLine("Failed to load graphics driver.");

		Utils.Mouse = DeviceService.GetFirstDevice<StandardMouse>().DeviceDriver as StandardMouse;

		// if (Utils.Mouse == null) HAL.Abort("Mouse not found.");
		if (Utils.Mouse == null) Console.WriteLine("Mouse not found.");

		Utils.BackColor = Color.Indigo;
		Utils.Mouse.SetScreenResolution(Display.Width, Display.Height);

		Mouse.Initialize();	
		WindowManager.Initialize();

		Taskbar = new Taskbar();
		Taskbar.Buttons.Add(new TaskbarButton(Taskbar, "Shutdown", Color.Blue, Color.White, Color.Navy,
			() => { Environment.Exit(0); return null; }));
		Taskbar.Buttons.Add(new TaskbarButton(Taskbar, "Reset", Color.Blue, Color.White, Color.Navy,
			() => { PCService.Reset(); return null; }));
		Taskbar.Buttons.Add(new TaskbarButton(Taskbar, "Settings", Color.Coral, Color.White, Color.Red,
			() => { WindowManager.Open(new Settings(70, 90, 400, 200, Color.MediumPurple, Color.Purple, Color.White)); return null; }));

		// Display.DrawWallpaper();
		for (; ; )
		{
			// Get current time
			var time = Platform.GetTime();
			// Clear screen
			Display.Clear(Color.FromArgb(22, 169, 253));

			// Display.DrawMosaLogo(40);
			// Initialize background labels
			var labels = new List<Label>
			{
				new("Current resolution is " + Display.Width + "x" + Display.Height, Display.DefaultFont, 10, 10, Color.White),
				new("FPS is " + FPSMeter.FPS, Display.DefaultFont, 10, 26, Color.White),
				new("Current font is " + Display.DefaultFont.Name, Display.DefaultFont, 10, 42, Color.White),
				new(
					(time.Hour < 10 ? "0" + time.Hour : time.Hour)
					+ ":" +
					(time.Minute < 10 ? "0" + time.Minute : time.Minute)
					+ ":" +
					(time.Second < 10 ? "0" + time.Second : time.Second),
					Display.DefaultFont, 10, 58, Color.White
				)
			};

			foreach (var label in labels) label.Draw();
			// Draw and update all windows
			WindowManager.Update();

			// Draw taskbar on top of everything else (except cursor) and update it
			Taskbar.Draw();
			Taskbar.Update();
						
			// Draw cursor
			Mouse.Draw();
			

			// Update graphics and FPS meter
			Display.Update();
			FPSMeter.Update(ref time);
			// FPSMeter.Update(ref time);
		}
		
	}

}
