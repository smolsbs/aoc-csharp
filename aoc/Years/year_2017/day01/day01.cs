using AdventOfCode.Runner.Attributes;

namespace Year2017.Day1;

[ProblemInfo(2017, 1, "Inverse Captcha")]
public class Day1: Problem<int,int>{
    List<int> _ret = new List<int>();
    public override void LoadInput() {
        var _inputData = ReadInputText().Trim();
        _ret = _inputData.Select(c => c - '0').ToList();
    }

    public override void CalculatePart1() {
        Part1 = 0;
        int _size = _ret.Count;
        for (var i = 0; i < _size; i++){
            if (_ret[i] == _ret[(i+1) % _size])
                Part1+=_ret[i];
        }
    }

    public override void CalculatePart2() {
        Part2 = 0;
        int _size = _ret.Count;
        int half = _size / 2;
        for (var i = 0; i < _size; i++){
            if (_ret[i] == _ret[(i+half) % _size])
                Part2+=_ret[i];
        }
    }

}
