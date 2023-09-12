global using System;
global using System.Collections.Generic;
global using System.Diagnostics;
global using System.IO;
global using System.Linq;

global using System.Security.Cryptography;
global using System.Text;
global using System.Text.RegularExpressions;
global using Superpower;
global using Superpower.Model;
global using Superpower.Parsers;
global using AdventOfCode.Runner;

namespace AdventOfCode;
internal class Program
{
	private static void Main(string[] args)
	{
		var runner = new AOCRunner();
		runner.RenderInteractiveMenu();
	}
}