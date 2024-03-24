using System.Shell;

namespace System;

public static class AppManager
{
	public static readonly IApp[] Applications =
	{
		new Shell(),
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