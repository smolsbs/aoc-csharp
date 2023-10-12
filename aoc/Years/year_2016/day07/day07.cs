using AdventOfCode.Runner.Attributes;

namespace Year2016.Day07;

[ProblemInfo(2016, 7, "Internet Protocol Version 7")]
public class Day07 : Problem<int, int>{

    private string[] _inputData = Array.Empty<string>();
    public override void LoadInput() {
        _inputData = ReadInputLines();
    }

    private bool FindABBA(List<string> values){
        foreach(var sub in values){
            for (int idx=0; idx < sub.Length-3; idx++){
                var aux = sub[idx..(idx+4)];
                if ((aux[0] == aux[3]) && (aux[1] == aux[2]) && (aux[0] != aux[1])){
                    return true;
                }
            }
        }
        return false;
    }

    private List<string> GetSubstrings(string line){
        var regex = new Regex(@"(\w+)");
        List<string> sub = new List<string>();

        foreach (Match m in regex.Matches(line)){
            sub.Add(m.ToString());
        }
        return sub;
    }

    private List<(char, char)> FindABA(List<string> values){
        List<(char, char)> _ret = new List<(char, char)>();
        foreach(var sub in values){
            for (int idx=0; idx < sub.Length-2; idx++){
                var aux = sub[idx..(idx+3)];
                if ((aux[0] == aux[2]) && (aux[0] != aux[1])){
                    _ret.Add((aux[0], aux[1]));
                }
            }
        }
        return _ret;
    }

    public override void CalculatePart1() {
        foreach(var line in _inputData){
            var strings = GetSubstrings(line);
            
            List<string> outside = strings.Where((v, idx) => idx % 2 == 0).ToList();
            List<string> inside = strings.Where((v, idx) => idx % 2 == 1).ToList();

            bool onOutside = FindABBA(outside);
            bool onInside = FindABBA(inside);

            if (onOutside && !onInside)
                Part1++;
        }
    }

    public override void CalculatePart2() {
        foreach(var line in _inputData){
            var strings = GetSubstrings(line);
            
            List<string> outside = strings.Where((v, idx) => idx % 2 == 0).ToList();
            List<string> inside = strings.Where((v, idx) => idx % 2 == 1).ToList();

            List<(char a, char b)> aba = FindABA(inside);
            List<(char b, char a)> bab = FindABA(outside);

            if (aba.Count == 0 || bab.Count == 0)
                continue;
            
            foreach(var ab in aba){
                if (bab.Any(ba => ba.a == ab.a && ba.b == ab.b)){
                    Part2++;
                    break;
                }
            }
        }
    }

}
