using AdventOfCode.Runner.Attributes;

namespace Year2017.Day10;

[ProblemInfo(2017, 10, "Knot Hash")]
public class Day10 : Problem<int, string> {
	int[] p1Data = Array.Empty<int>();
	int[] p2Data = Array.Empty<int>();
	int[] values = Enumerable.Range(0, 256).ToArray();

	public override void LoadInput() {
		p1Data = ReadInputText().Split(',').Select(v => int.Parse(v)).ToArray();
		p2Data = ReadInputText().Select(v => (int)v).Concat(new int[]{17, 31, 73, 47, 23}).ToArray();

	}
	public override void CalculatePart1() {
		Knot(p1Data,1);
		Part1 = values[0] * values[1];
	}
	public override void CalculatePart2() {
		values = Enumerable.Range(0, 256).ToArray();
		Knot(p2Data,64);

		StringBuilder hash = new StringBuilder();

		for (int i = 0; i < 16; i++) {
			var aux = values[(16 * i)..(16 * (i + 1))].Aggregate((a, b) => a ^ b);
			hash.Append(Convert.ToByte((decimal)aux).ToString("X2"));
		}
		Part2 = hash.ToString().ToLower();
	}

	private void Knot(int[] keys, int rounds) {
		int currPos = 0;
		int skipLen = 0;
		int round = 0;

		while (round < rounds) { 
			foreach(int l in keys) {
				int[] slice = new int[l];

				for(int i = 0; i < l; i++) {
					slice[i] = values[(currPos + i) % 256];
				}
				slice = slice.Reverse().ToArray();
				for(int i = 0; i < l; i++) {
					values[(currPos + i) % 256] = slice[i];
				}
				currPos = (currPos + l + skipLen) % 256;
				skipLen++;
			}
			round++;
		}

	}

}