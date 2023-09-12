namespace Year2016.Day01;
using System.Drawing;
public class Day01{
    public static void Run(string Path){
        IEnumerable<string> cmds = File.ReadAllText(Path).Split(", ");
        int compass = 0; // 0 for N, 1 for E, 2 for S, 3 for W  
        Point coords = new Point(0,0);
        HashSet<string> visited = new HashSet<string>(){coords.ToString()};
        int p2 = -1;

        foreach(var c in cmds){
            
            if(c[0] == 'R') 
                compass = (compass + 1).mod(4);
            else 
                compass = (compass - 1).mod(4);
            
            for (var i=0; i<int.Parse(c.Substring(1)); i++){
                switch(compass){
                    case 0: coords.Y++; break;
                    case 1: coords.X++; break;
                    case 2: coords.Y--; break;
                    case 3: coords.X--; break;
                }
                if (visited.Add(coords.ToString()) == false && p2 == -1)
                    p2 = Math.Abs(coords.X) + Math.Abs(coords.Y);
            }
        }
        int p1 = Math.Abs(coords.X) + Math.Abs(coords.Y);
        Console.WriteLine($"Part 1: {p1}\nPart 2: {p2}");
    }
}