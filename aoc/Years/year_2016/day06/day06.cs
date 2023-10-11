using AdventOfCode.Runner.Attributes;

namespace Year2016.Day02;

[ProblemInfo(2016, 6, "Signals and Noise")]
public class Day06 : Problem{

    private string[] _inputData = Array.Empty<string>();
    public override void LoadInput() {
        _inputData = ReadInputLines();
    }
    public override void CalculatePart1() {

        var linqFU1 = Enumerable.Range(0, _inputData.First().Length)
    						 .Select(c => _inputData.Select(line => line[c]))
    						 .Select(c => string.Join("", c))
							 .Select(v => v.GroupBy(c => c).MaxBy(g => g.Count() )!.Key);

        Part1 = string.Join("", linqFU1);
        return;
    }
    public override void CalculatePart2() {
        var linqFU2 = Enumerable.Range(0, _inputData.First().Length)
    						 .Select(c => _inputData.Select(line => line[c]))
    						 .Select(c => string.Join("", c))
							 .Select(v => v.GroupBy(c => c).MinBy(g => g.Count() )!.Key);

        Part2 = string.Join("", linqFU2);
    }

}