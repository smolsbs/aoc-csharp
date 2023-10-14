using AdventOfCode.Runner.Attributes;

namespace Year2016.Day12;

[ProblemInfo(2016, 12, "Leonardo's Monorail")]
public class Day12: Problem<int,int>{
    private string[] _inputData = Array.Empty<string>();

    public override void LoadInput() {
        _inputData = ReadInputLines();
    }

    public override void CalculatePart1() {
        var machine = Assembunny.Assembunny.Create(_inputData);
        machine.Run();

        Part1 = machine.GetRegister('a');
    }

    public override void CalculatePart2() {
        var machine = Assembunny.Assembunny.Create(_inputData);
        machine.SetRegister('c', 1);
        machine.Run();

        Part2 = machine.GetRegister('a');
    }
}

