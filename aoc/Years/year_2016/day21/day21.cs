using AdventOfCode.Runner.Attributes;

namespace Year2016.Day21;

[ProblemInfo(2016, 21, "Scrambled Letters and Hash")]
public class Day21: Problem{

    private List<char> password = "abcdefgh".ToCharArray().ToList();
    private List<char> drowssap = "fbgdceah".ToCharArray().ToList();
    private string[] steps = Array.Empty<string>();

    public override void LoadInput() {
        steps = ReadInputLines();
    }

    public override void CalculatePart1() {
        foreach (var line in steps) {
            var cmd = line.Split(' ');
            switch (cmd[0]){
                case "swap":
                    Swap(cmd, ref password);
                    break;
                case "move":
                    Move(cmd, ref password);
                    break;
                case "rotate":
                    Rotate(cmd, ref password);
                    break;
                case "reverse":
                    Reverse(cmd, ref password);
                    break;
            }
        }
        Part1 = password.Stringify();
    }

    public override void CalculatePart2() {
        password = drowssap;
        foreach (var enil in steps.Reverse()){
            var cmd = enil.Split(' ');
            switch (cmd[0]){
                case "swap":
                    Swap(cmd, ref drowssap);
                    break;
                case "move":
                    Move(cmd, ref drowssap, true);
                    break;
                case "rotate":
                    Rotate(cmd, ref drowssap, true);
                    break;
                case "reverse":
                    Reverse(cmd, ref drowssap);
                    break;
            }
        }
        Part2 = drowssap.Stringify();
    }

    private void Move(string[] cmd, ref List<char> pw, bool reverse=false){
        int from, to;
        if (reverse){
            from = int.Parse(cmd[5]);
            to = int.Parse(cmd[2]);
        }else{
            from = int.Parse(cmd[2]);
            to = int.Parse(cmd[5]);
        }

        var store = pw[from];
        pw.RemoveAt(from);
        pw.Insert(to, store);
    }

    private void Rotate(string[] cmd, ref List<char> pw, bool reverse=false){
        List<char> newPW = new List<char>();
        int rev = 1;
        if (reverse)
            rev = -1;
        
        if (cmd[1] == "left"){
            int steps = int.Parse(cmd[2]);
            for (var i = 0; i < pw.Count; i++){
                newPW.Add(pw[(i+steps*rev).mod(pw.Count)]);
            }
        } else if (cmd[1] == "right") {
            int steps = int.Parse(cmd[2]);
            for (var i = 0; i < pw.Count; i++){
                newPW.Add(pw[(i-steps*rev).mod(pw.Count)]);
            }
        } else {
            int steps;
            if (reverse){
                int value =  pw.IndexOf(cmd[6].ToChar());
                steps = RevereRotateBy(value);
            }else{
                steps = pw.IndexOf(cmd[6].ToChar()) + 1;
                if (steps > 4)
                    steps++;
            }
            for (var i = 0; i < pw.Count; i++){
                newPW.Add(pw[(i-steps*rev).mod(pw.Count)]);
            }
        }

        pw = newPW;
    }

    private void Reverse(string[] cmd, ref List<char> pw){
        int pos1 = int.Parse(cmd[2]);
        int pos2 = int.Parse(cmd[4]);

        var left = pw.Take(pos1);
        var middle = pw.Skip(pos1).Take(pos2-pos1+1);
        var right = pw.Skip(pos2+1);

        middle = middle.Reverse();

        pw = left.Concat(middle).Concat(right).ToList();
    }

    private void Swap(string[] cmd, ref List<char> pw){
        int a,b;
        if (cmd[1] == "position"){
            a = int.Parse(cmd[2]);
            b = int.Parse(cmd[5]);
        } else {
            a = pw.IndexOf(cmd[2].ToChar());
            b = pw.IndexOf(cmd[5].ToChar());
        }
        (pw[a], pw[b]) = (pw[b], pw[a]);
    }

    private Dictionary<int, int> reverseRot = new Dictionary<int, int>() { {1,1}, {3,2}, {5,3}, {7,4}, {2,6}, {4,7}, {6,8}, {0,9} };
    private int RevereRotateBy(int value){
        return reverseRot[value];
    }
}
