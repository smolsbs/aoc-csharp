using AdventOfCode.Runner.Attributes;

namespace Year2017.Day9;

[ProblemInfo(2017, 9, "Stream Processing")]
public class Day8 : Problem<int,int> {
	string data = "";
	public override void LoadInput() {
		data = ReadInputText();
	}
	public override void CalculatePart1() {
		Part1 = Score(data);
	}
	public override void CalculatePart2() {
		Part2 = Score(data, true);
	}

	private int Score(string data, bool p2=false) {
		int i = 0;
		int state = 0;
		int totalScore = 0;
		int score = 1;
		int garbageCount = 0;
		while (i < data.Length) {

			switch (state) {
				//outside garbage
				case 0:
					if (data[i] == '<') {
						state = 1;
					} else if (data[i] == '{') {
						totalScore += score;
						score++;
					} else if (data[i] == '}') {
						score--;
					}
					break;
				//inside garbage
				case 1:
					if (data[i] == '!') {
						i++;
					} else if (data[i] == '>') {
						state = 0;
					} else {
						garbageCount++;
					}
					break;
				default:
					break;
			}
			i++;
		}

		if (p2)
			return garbageCount;
		return totalScore;
	}

}