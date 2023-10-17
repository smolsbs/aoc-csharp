using AdventOfCode.Runner.Attributes;

namespace Year2016.Day11;

[ProblemInfo(2016, 11, "Radioisotope Thermoelectric Generators")]
public class Day11: Problem<int, int>{
    private List<int> floors = new List<int>();

    public override void LoadInput() {
        Regex parser = new Regex(@"a (?<thing>\w+(?:|-\w+) \w+)");
        var _input = ReadInputLines();
        foreach( (string line, int idx) in _input.Select( (v,i) => (v,i) )){
            var _match = parser.Matches(line);
            floors.Add(_match.Count());
        }
    }
    public override void CalculatePart1() {

        Part1 = GetMoves(floors.ToList());

    }
    public override void CalculatePart2() {
        floors[0] += 4;
        Part2 = GetMoves(floors.ToList());
    }

    private int GetMoves(List<int> state){
        int nMoves = 0;
        int f = 0;
        while (state[^1] != state.Sum()){
            if (state[f] == 0){
                f++;
                continue;
            }
            nMoves += 2 * (state[f] - 1) - 1;
            state[f+1] += state[f];
            state[f] = 0;
        }

        return nMoves;
    }

}
