using System;
namespace year_2022.day_01;


public class Day01
{
    public static void Run(string Path)
    {
        string[] usrIn = File.ReadAllText(Path);
        int[] calories = [];
        int c = 0;
        foreach (string line in usrIn)
        {
            if (line != "")
            {
                c += int.Parse(line)
            }
            else
            {
                calories.add(c)
                c = 0
            }
        }



        return;
    }
}
