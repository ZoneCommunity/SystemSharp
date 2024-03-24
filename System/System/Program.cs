// Copyright (c) MOSA Project. Licensed under the New BSD License.

using System;
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

	public static void EntryPoint()
	{
		Debug.WriteLine("Program::EntryPoint()");

		Console.ResetColor();
		Console.Clear();
		Console.WriteLine("System has booted!");
		Console.WriteLine("This version is 0.0.1_32bit");

		Console.WriteLine();
		Console.WriteLine("System Shell");
		Console.WriteLine("Enter \"quit\" to exit the shell.");

		while (true)
		{
			Console.Write("> ");

			var cmd = Console.ReadLine();

			if (cmd == "quit")
				break;
		}

		for (; ; )
		{ }
	}
}
