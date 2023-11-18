using AdventOfCode.Runner.Attributes;

namespace Year2016.Day02;

[ProblemInfo(2017, 2, "Corruption Checksum")]
public class DayTemplate : Problem<int, int>{
	List<List<int>> _inputData = new List<List<int>>();

	public override void LoadInput() {
		_inputData = ReadInputLines().Select(x => x.Split('\t').Select(v => int.Parse(v)).Order().ToList()).ToList();
	}
	public override void CalculatePart1() {
		foreach (var item in _inputData) {
			Part1 += item[^1] - item[0];
		} 
		return;
	}
	public override void CalculatePart2() {
		foreach (var item in _inputData)
		{
			int idx = 0;
			int offset = idx + 1;
			while (true)
			{
				if (item[offset] % item[idx] == 0)
				{
					Part2 += item[offset] / item[idx];
					break;
				}
				offset++;
				if (offset == item.Count)
				{
					idx += 1;
					offset = idx + 1;
				}
			}
		}
	}

}
