// Copyright (c) MOSA Project. Licensed under the New BSD License.

using System;
using Mosa.DeviceSystem.HardwareAbstraction;
using Mosa.DeviceSystem.Services;
using Mosa.Kernel.BareMetal;
using Mosa.Runtime.Plug;

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

	public static void EntryPoint()
	{
		Debug.WriteLine("Program::EntryPoint()");

		Console.ResetColor();
		// Console.BackgroundColor = ConsoleColor.Black;
		// Console.ForegroundColor = ConsoleColor.White;

		Console.Clear();
		Console.WriteLine("System has booted!");
		Console.WriteLine("Running version 0.0.1");
		
		DeviceService = Mosa.Kernel.BareMetal.Kernel.ServiceManager.GetFirstService<DeviceService>();

		AppManager.Execute("Shell");

		Console.WriteLine();
		Console.WriteLine("Shutting down...");

		var pcService = Mosa.Kernel.BareMetal.Kernel.ServiceManager.GetFirstService<PCService>();
		var success = pcService.Shutdown();
		

		if (!success) {
			Console.WriteLine("Error while trying to shut down.");
		}

		// Let's just re-run the shell for now
		// AppManager.Execute("Shell");


		for (; ; ) HAL.Yield();

	}
}
