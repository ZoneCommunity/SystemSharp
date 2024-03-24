// Copyright (c) MOSA Project. Licensed under the New BSD License.

using System;
using Mosa.Kernel.BareMetal;

namespace System.Shell.Apps;

public class Hello : IApp
{
	public string Name => "hello";

	public string Description => "Prints \"Hello World!\"";

	public void Execute()
	{
		Console.WriteLine("Hello World!");
	}
}