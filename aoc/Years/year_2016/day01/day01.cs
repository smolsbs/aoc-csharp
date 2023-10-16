using AdventOfCode.Runner.Attributes;
using System.Drawing;

namespace Year2016.Day02;

[ProblemInfo(2016, 1, "No Time for a Taxicab")]
public class Day01 : Problem<int, int>{
    private string[] _inputData = Array.Empty<string>();
    private Point position = new Point(0,0);
    private HashSet<string> visited = new HashSet<string>();

    public override void LoadInput() {
        _inputData = ReadInputText().Split(", ");
    }

    public override void CalculatePart1() {
        Part1 = Run();
    }

    public override void CalculatePart2() {
        position = new Point(0,0);
        visited = new HashSet<string>();
        Part2 = Run(true);
    }

    private int Run(bool p2=false){
        visited.Add(position.ToString());
        int compass = 0; // 0 for N, 1 for E, 2 for S, 3 for W  

        foreach(var cmd in _inputData){
            
            if(cmd[0] == 'R') 
                compass = (compass + 1).mod(4);
            else 
                compass = (compass - 1).mod(4);
            
            for (var i=0; i<int.Parse(cmd.Substring(1)); i++){
                switch(compass){
                    case 0: position.Y++; break;
                    case 1: position.X++; break;
                    case 2: position.Y--; break;
                    case 3: position.X--; break;
                }
                if (visited.Add(position.ToString()) == false && p2)
                    return Math.Abs(position.X) + Math.Abs(position.Y);
            }
        }
        return Math.Abs(position.X) + Math.Abs(position.Y);
    }

}
