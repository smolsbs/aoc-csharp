using AdventOfCode.Runner.Attributes;

namespace Year2016.Day23;

[ProblemInfo(2016, 23, "Safe Cracking")]
public class Day23: Problem<int,int>{
    private string[] _inputData = Array.Empty<string>();

    public override void LoadInput() {
        _inputData = ReadInputLines();
    }

    public override void CalculatePart1() {
        var machine = Assembunny.Assembunny.Create(_inputData);
        machine.SetRegister('a', 7);
        machine.Run();
        Part1 = machine.GetRegister('a');
    }

    public override void CalculatePart2() {
        var machine = Assembunny.Assembunny.Create(_inputData);
        machine.SetRegister('a', 12);
        machine.Run();
        Part2 = machine.GetRegister('a');
    }
}
