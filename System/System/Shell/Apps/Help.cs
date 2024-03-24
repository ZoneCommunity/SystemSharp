// Copyright (c) MOSA Project. Licensed under the New BSD License.

using System;
using Mosa.Kernel.BareMetal;

namespace System.Shell.Apps;

public class Help : IApp
{
	public string Name => "help";

	public string Description => "Shows information about all commands.";

	public void Execute()
	{
        foreach (var app in AppManager.Applications)
        {
            // Calculate the number of spaces needed after the app name
            int spacesCount = 9 - app.Name.Length;
            // Construct the output string with the desired formatting
            string output = $"{app.Name}{new string(' ', spacesCount)} > {app.Description}";
            Console.WriteLine(output);
        }
	}
}