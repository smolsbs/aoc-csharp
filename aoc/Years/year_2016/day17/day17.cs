using AdventOfCode.Runner.Attributes;
using System.Drawing;

namespace Year2016.Day17;

[ProblemInfo(2016, 17, "Two Steps Forward")]
public class Day17: Problem<string, int?>{
    private string _inputData = "";

    private string[] Steps = {"U","D","L","R"};
    

    public override void LoadInput() {
        _inputData = ReadInputText().Trim();
    }
    public override void CalculatePart1() {
        Part1 = Walk((0,0), Array.Empty<string>(), Enumerable.MinBy);
    }

    public override void CalculatePart2() {
        Part2 = Walk((0,0), Array.Empty<string>(), Enumerable.MaxBy)?.Length;
    }

    private string GetState(string state){
        var aux = MD5.HashData(Encoding.ASCII.GetBytes(state));
        return BitConverter.ToString(aux).Replace("-", String.Empty).ToLower()[..4];
    }

    private string? Walk((int,int) pos, string[] path, Func<IEnumerable<string>, Func<string, int>, string?> retBy){
        if (pos == (3,-3))
            return path!.JoinStrings();

         var toHash = $"{_inputData}{path.JoinStrings()}";
         var state = GetState(toHash);
         List<string> directions = new List<string>(4);
        

         for (var i = 0; i < 4; i++){
             var c = state[i];
             if (isUnlocked(c) && NewPosition(pos, i, out var nPos)){
                 var nPath = path.Append(Steps[i]);
                 var a = Walk(nPos, nPath.ToArray(), retBy); 
                 if (a != null)
                     directions.Add(a);
             }
         }
         if (directions.Count != 0)
             return retBy(directions, c => c.Length);

         return null;
    }

    private bool isUnlocked(char c){
        return c switch {
            'b' or 'c' or 'd' or 'e' or 'f' => true,
            _ => false
        };
    }

    private bool NewPosition((int x, int y) oldPos, int dir, out (int x, int y) nPos){
        
        (int x, int y) a = dir switch {
            0 => (oldPos.x, oldPos.y + 1),
            1 => (oldPos.x, oldPos.y - 1),
            2 => (oldPos.x - 1, oldPos.y),
            3 => (oldPos.x + 1, oldPos.y),
            _ => oldPos
        };
        if ((a.x >= 0 && a.x < 4) && (a.y > -4 && a.y <= 0 )) {
            nPos = a;
            return true;
        }
        nPos = oldPos;
        return false;
        
    }

}
