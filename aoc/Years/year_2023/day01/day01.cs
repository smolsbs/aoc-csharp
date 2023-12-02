using AdventOfCode.Runner.Attributes;
using System.Net.Http.Headers;

namespace Year2023.Day1;

[ProblemInfo(2023, 1, "Trebuchet?!")]
public class Day1 : Problem<int, int> {
	string[] _inputData = Array.Empty<string>();
	Regex reNums = new Regex(@"\d");
	Dictionary<string, int> dictNums = new Dictionary<string, int>()
	{
		{"one", 1},{"two", 2},{"three", 3},
		{"four", 4},{"five", 5},{"six", 6},
		{"seven", 7},{"eight", 8},{"nine", 9}
	};

	public override void LoadInput() {
		_inputData = ReadInputLines();
		return;
	}
	public override void CalculatePart1() {
		foreach(string line in _inputData) {
			Part1 += GetNumbers(line);
		}
	}
	public override void CalculatePart2() {
		foreach(string line in _inputData) {
			Part2 += GetNumbers(line, true);
		}
	}

	private int GetNumbers(string line, bool part2 = false) {
		MatchCollection nums = reNums.Matches(line);
		List<(int number, int pos)> matches = nums.Select(x => (int.Parse(x.Value), x.Index)).ToList();

		int left = 0, right = 0, rightPos = 0, leftPos = 0;

		if (matches.Count == 1) {
			left = matches[0].number * 10;
			leftPos = matches[0].pos;
			right = matches[0].number;
			rightPos = matches[0].pos;
		} else {
			left = matches[0].number * 10;
			leftPos = matches[0].pos;
			right = matches[^1].number;
			rightPos = matches[^1].pos;
		}

		if(!part2)
			return left+right;

		foreach(var k in dictNums.Keys) {
			var r = new Regex(k);
			var aux = r.Matches(line);

			if (aux.Count == 0)
					continue;

			if (aux.First().Index < leftPos) {
				leftPos = aux.First().Index;
				left = dictNums[aux.First().Value]*10;
			}

			if(aux.Last().Index > rightPos) {
				rightPos = aux.Last().Index;
				right = dictNums[aux.Last().Value];
			}
		}

		return left + right;
	}

}
