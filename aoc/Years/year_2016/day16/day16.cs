using AdventOfCode.Runner.Attributes;
using System.Diagnostics;

namespace Year2016.Day16;

[ProblemInfo(2016, 16, "Dragon Checksum")]
public class Day16: Problem{
    private string _inputData = "";
    private const int P1Len = 272;
    private const int P2Len = 35651584;

    public override void LoadInput() {
        _inputData = ReadInputText().TrimEnd();
    }
    public override void CalculatePart1() {
        Part1 = ExpandAndCheck(_inputData, P1Len);
    }
    public override void CalculatePart2() {
        Part2 = ExpandAndCheck(_inputData, P2Len);
    }

    private string ExpandAndCheck(string a, int len){
        var checksum = _inputData;
        while (checksum.Length < len){
            checksum = Expand(checksum);
        }
        checksum = checksum[..len];
        return Check(checksum);
    }

    private string Expand(string a){
        var b = a.Reverse().Select(c => c == '1' ? '0' :  '1');
        return $"{a}0{string.Join("", b)}";
    }

    private string Check(string data){
        var aux = data;
        while (true){
            aux = aux.Chunk(2).Select(c => c[0] == c[1] ? "1" : "0").JoinStrings();
            if (aux.Length % 2 == 1)
                return aux;
        }
    }
}
