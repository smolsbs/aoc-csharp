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
using Year2016.Day01;

public static class Utils{
    public static int mod(this int a, int n)
    {
        return ((a % n) + n) % n;
    }
}

public class AdventOfCode
{
    public static void Main()
    {
        // Day01.Run("./year_2016/day01/day01.in");
        Day02.Run("./year_2016/day02/day02.in");
    }
}