using AdventOfCode.Runner.Attributes;

namespace Year2017.Day6;

[ProblemInfo(2017, 6, "Memory Reallocation")]
public class Day6 : Problem<int, int> {
    List<int> banks = new List<int>();
    Dictionary<string, int> seen = new Dictionary<string, int>();

    public override void LoadInput() {
        banks = ReadInputText().Split('\t').Select(v => int.Parse(v)).ToList();
        return;
    }
    public override void CalculatePart1() {
        int banksSize = banks.Count;
        int counter = 0;
        SaveState(banks, counter);
        while (true) {
            counter++;
            //get biggest value
            int idx = GetBiggestBank(banks);
            int value = banks[idx];
            banks[idx] = 0;

            // spread the value of said bank
            for (int i = 1; i <= value; i++) {
                banks[(idx + i) % banksSize]++;
            }

            // try and record the state
            if (!SaveState(banks, counter))
                break;
        }
        Part1 = counter;
    }
    public override void CalculatePart2() {
        string k = string.Join("|", banks);
        Part2 = Part1 - seen[k];
    }

    private int GetBiggestBank(List<int> bank) {
        return bank.Select((v, idx) => (v, idx)).MaxBy(v => v.v).idx;
    }

    private bool SaveState(List<int> arr, int counter) {
        return seen.TryAdd(string.Join("|", arr), counter);
    }

}