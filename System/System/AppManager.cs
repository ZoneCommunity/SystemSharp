using System.Shell;
using System.Shell.Apps;

namespace System;

public static class AppManager
{
	public static readonly IApp[] Applications =
	{
		new Shell.Shell(),
        new Mem(),
        new Hello(),
        new Help()
	};

	public static bool Execute(string name)
	{
		foreach (var app in Applications)
		{
			if (app.Name.ToLower() == name.ToLower())
			{
				app.Execute();
				return true;
			}
		}

		return false;
	}
}