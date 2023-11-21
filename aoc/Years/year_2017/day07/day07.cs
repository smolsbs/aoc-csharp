using AdventOfCode.Runner.Attributes;

namespace Year2017.Day7;

[ProblemInfo(2017, 7, "Recursive Circus")]
public class Day7 : Problem<string,int> {
	Regex parser = new Regex(@"(?<id>\w+) \((?<weight>\d+)\)($| -> (?<children>.*?$))");
	List<Program> nodes = new List<Program>();

	public override void LoadInput() {
		foreach (var line in ReadInputLines()) {
			var matches = parser.Match(line);
			string _id = matches.Groups["id"].Value;
			int weight = int.Parse(matches.Groups["weight"].Value);

			Program? node = null;

			var aux = nodes.Find(n => n.id == _id);
			if (aux != null) {
				node = aux;
				node.weight = weight;
			} else {
				node = CreateNode(_id, weight);
				nodes.Add(node);
			}

			if (matches.Groups["children"].Success) {
				string[] children = matches.Groups["children"].Value.Split(", ").ToArray();
				foreach (string c in children) {
					Program? cNode = null;
					var found = nodes.Find(n => n.id == c);
					if (found != null) {
						cNode = found;
						cNode.parent = node;
					} else {
						cNode = CreateNode(c, 0);
						cNode.parent = node;
						nodes.Add(cNode);
					}
					node.childs.Add(cNode);
				}
			}
		}
		return;
	}

	Program? bottomNode = null;
	public override void CalculatePart1() {
		bottomNode = nodes.Find(n => n.parent == null)!;
		Part1 = bottomNode.id;
	}

	public override void CalculatePart2() {
		Console.WriteLine("\n");
		CheckBalance(bottomNode!);
		return;
	}

	private Program CreateNode(string id, int weight) {
		Program node = new Program(id);
		node.weight = weight;
		return node;
	}

	private int CheckBalance(Program node) {
		if (node.childs.Count == 0) {
			return node.weight;
		}

		List<(Program, int)> sums = new List<(Program, int)>();
		foreach (var child in node.childs) {
			sums.Add((child, CheckBalance(child)));
		}
		int _sum = sums.Select(v => v.Item2).Sum();
		if (sums.GroupBy(v => v.Item2).Count() != 1) {
			(Program node, int v) pChild = sums.GroupBy(v => v.Item2)
											   .First(x => x.Count() == 1)
											   .First();
			(Program node, int v) bChild = sums.GroupBy(v => v.Item2)
											   .First(x => x.Count() != 1)
											   .First();

			int sign = pChild.v < bChild.v ? 1 : -1;

			if (Part2 == 0) {
				Part2 = pChild.node.weight + (sign * Math.Abs(pChild.v - bChild.v));
			}

		}
		return _sum + node.weight;
	}
}

public class Program {
	public string id;
	public int weight;
	public Program? parent;
	public List<Program> childs;

	public Program(string id) {
		this.id = id;
		this.childs = new List<Program>();
	}

	public override string ToString() {
		var aux = parent == null ? "None" : parent.id;
		return $"{id}, {weight}, {aux}, {childs.Count}";
	}
}
