using AdventOfCode.Runner.Attributes;

using System.Security.Cryptography;

namespace Year2016.Day02;

[ProblemInfo(2016, 5, "How About a Nice Game of Chess?")]
public class Day05 : Problem{

    private string _inputData = "";
    private char[] pw = new char[8];
    private Dictionary<int, char?> forP2 = new Dictionary<int, char?>();
    public override void LoadInput() {
        _inputData = ReadInputText();
        Hasher();
    }
    public override void CalculatePart1() {

        Part1 = string.Join("", pw);
        return;
    }
    public override void CalculatePart2() {
        Part2 = string.Join("", forP2.Values);
        return;
    }


    private void Hasher(){
        // preparing the dict
        for (var i = 0; i <8; i++)
            forP2.Add(i, null);
        int c = 0;
        int globalCounter = 0;
        var hasher = MD5.Create();
        int pos;
        bool p1Running = true, p2Running = true;

        while (p1Running || p2Running){

            var toHash = Encoding.ASCII.GetBytes($"{_inputData}{globalCounter}");
            string hashed = BitConverter.ToString(hasher.ComputeHash(toHash)).Replace("-", string.Empty);
            if (hashed[..5] == "00000"){
                if (c < 8){
                    pw[c] = hashed[5];
                    c += 1;
                }
                if (int.TryParse(hashed[5].ToString(), out pos) && pos < 8 && forP2[pos] == null){
                    forP2[pos] = hashed[6];
                }
            }
            if (c == 8)
                p1Running = !p1Running;
            
            if (forP2.Select(v => v.Value).Any(c => c == null) == false)
                p2Running = !p2Running;

            globalCounter++;
        }

    }

}
