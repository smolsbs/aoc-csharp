using AdventOfCode.Runner.Attributes;

namespace Year2023.Day6;

[ProblemInfo(2023, 6, "Wait For It")]
public class Day6: Problem<long, long>{
	List<(long time, long dist)> nums = new();
	List<long> results = new();
	public override void LoadInput() {
		var data = ReadInputLines();
		
		var times = data[0].Split(':')[1].Split(' ').Where(v => v.Length > 0).Select(long.Parse).ToList();
		var dists = data[1].Split(':')[1].Split(' ').Where(v => v.Length > 0).Select(long.Parse).ToList();

		times.Add(long.Parse(string.Join("", times.Select(x => $"{x}"))));
		dists.Add(long.Parse(string.Join("", dists.Select(x => $"{x}"))));

		nums = times.Zip(dists).ToList();
		CalculateScore();
	}

	private void CalculateScore() {
		for (var i = 0; i<nums.Count; i++) {
			long start = FindStart(nums[i].time, nums[i].dist);
			long end = nums[i].time - start;
			results.Add(end - start + 1);
		}
	}

	private long FindStart(long time, long dist) {
		for ( long i = dist/time; i < time/2; i++) {
			long run_dist = i * (time - i);
			if (run_dist > dist) {
				return i;
			}
		}
		return -1;
	}

	public override void CalculatePart1() {
		Part1 = results.Take(results.Count - 1).Aggregate((a, b) => a * b);
	}
	public override void CalculatePart2() {
		Part2 = results.Last();
	}

}