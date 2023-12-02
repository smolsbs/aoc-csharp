using AdventOfCode.Runner.Attributes;
using aoc.Years.year_2023.day02;

namespace Year2023.Day2;

[ProblemInfo(2023, 2, "Cube Conundrum")]
public class Day2 : Problem<int, int> {
	private GameList games = new GameList();

	public override void LoadInput() {
		string[] _input = ReadInputLines();
		games.LoadGames(_input);
	}

	public override void CalculatePart1() {
		Part1 = games.game_list.Where(GameList.Game.CheckIfValidGame)
							   .Select(x => x.id)
							   .Sum();
	}

	public override void CalculatePart2() {
		Part2 = games.game_list.Select(GameList.Game.GetMinimumDices)
							   .Select(rgb => rgb.red * rgb.green * rgb.blue)
							   .Sum();
	}
}