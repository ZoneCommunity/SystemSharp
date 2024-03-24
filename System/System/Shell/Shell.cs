using System;

namespace System.Shell;

public class Shell : IApp
{
	public string Name => "Shell";

	public string Description => "Runs the interactive shell.";

	public void Execute()
	{
		Console.WriteLine();
		Console.WriteLine("Shell environment loaded");
		Console.WriteLine("Enter \"quit\" to exit the shell.");

		while (true)
		{
			Console.Write("#system> ");

			var cmd = Console.ReadLine();

			if (cmd == "quit")
				break;

			if (!AppManager.Execute(cmd))
			{
				Console.WriteLine("Bad command or file name '" + cmd + "'");
			}

            Console.WriteLine();
		}
	}
}