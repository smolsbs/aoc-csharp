using AdventOfCode.Runner.Attributes;

namespace Year2016.Day03;

[ProblemInfo(2016, 3, "Squares With Three Sides")]
public class Day03 : Problem<int, int>{

    private string[] _inputData = Array.Empty<string>();

    public override void LoadInput() {
        _inputData = ReadInputLines();
    }
    public override void CalculatePart1() {
        Part1 = 0;
        foreach (var line in _inputData) {
            List<int> triangle = GetOrderedInts(line);

            if (IsValidTriangle(triangle))
                Part1++;
        }
    }
    public override void CalculatePart2() {
        Part2 = 0;

        // alien readable code

        Part2 = _inputData.Select(GetInts).Chunk(3).SelectMany(ch => Enumerable.Range(0, 3).Select(col => ch.Select(r => r.Skip(col).First()).Order().ToList())).Count(IsValidTriangle);

        // human readable code

        // for(int i = 0; i < _inputData.Length - 2; i+=3){
        //     List<List<int>> things = new List<List<int>>{ GetInts(_inputData[i]), GetInts(_inputData[i+1]), GetInts(_inputData[i+2])};
        //     for (int j = 0; j < 3; j++ ){
        //         var t = new List<int>{things[0][j], things[1][j], things[2][j]}.Order().ToList();
        //         if (IsValidTriangle(t))
        //             Part2++;
        //     }
        // }
        // return;
    }


     private List<int> GetInts(string value){
        return value.Split(' ')
                    .Where(x => x.Length > 0)
                    .Select(int.Parse)
                    .ToList();
    }


    private List<int> GetOrderedInts(string value){
        return value.Split(' ')
                    .Where(x => x.Length > 0)
                    .Select(int.Parse)
                    .Order()
                    .ToList();
    }


    private bool IsValidTriangle(List<int> triangle) {
        return triangle[0] + triangle[1] > triangle[2];
    }

}