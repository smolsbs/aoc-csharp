using AdventOfCode.Runner.Attributes;

namespace Year2017.Day4;

[ProblemInfo(2017, 4, "High-Entropy Passphrases")]
public class Day4: Problem<int, int>{
    string[] data = Array.Empty<string>();
    List<string> filtered = new List<string>();
    public override void LoadInput() {
        data = ReadInputLines();
    }
    public override void CalculatePart1() {
        Part1 = 0;
        foreach (var line in data)
        {
            Part1 += line.Split(" ").GroupBy(x => x).All(x => x.Count() == 1) ? 1 : 0;
            filtered.Add(line);
        }
    }
    public override void CalculatePart2() {

        Part2 = 0;
        foreach (var line in filtered)
        {
            string[] words = line.Split(" ")
                             .Select(w => w.OrderBy(x => x)
                                           .Stringify())
                             .OrderBy(x => x.Length)
                             .ToArray();
            Part2 += words.GroupBy(x => x).All(x => x.Count() == 1) ? 1 : 0;
        }
    }

}