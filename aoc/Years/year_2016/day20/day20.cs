using AdventOfCode.Runner.Attributes;

namespace Year2016.Day20;

[ProblemInfo(2016, 20, "Firewall Rules")]
public class Day20: Problem<uint, uint>{

    private List<uint[]> blacklistRanges = new List<uint[]>();
    private string[] _inputData = Array.Empty<string>();

    public Blacklist blacklist = new Blacklist();

    public override void LoadInput() {
        _inputData = ReadInputLines();
    }
    public override void CalculatePart1() {
        foreach(var line in _inputData){
            var aux = line.Split("-").Select(c => uint.Parse(c)).ToArray();
            // Console.WriteLine((aux[0], aux[1]));
            blacklist.addRange((aux[0], aux[1])); 
        }
        blacklist.range = blacklist.range.OrderBy( r => r.v1 ).ToList();

        while (true){
            int blSize = blacklist.count;
            Blacklist aux = new Blacklist();

            foreach (var range in blacklist.range) {
                aux.addRange( range );
            }
            var newSize = aux.count;
            blacklist = aux;

            if (newSize == blSize)
                break;
        }
        
        blacklist.range = blacklist.range.OrderBy( r => r.v1 ).ToList();
        Part1 = blacklist.range[0].v2 + 1; 
    }
    public override void CalculatePart2() {
        uint allowedIPs = 0;
        
        for (var idx = 0; idx < blacklist.count -1; idx++){
            var diff = blacklist.range[idx+1].v1 - blacklist.range[idx].v2 - 1;
            allowedIPs += diff;
        }
        Part2 = allowedIPs;
    }
}


public class Blacklist{
    public List<(uint v1, uint v2)> range = new List<(uint, uint)>();
    public int count => range.Count;

    public void addRange((uint lb, uint ub) values){
        if (range.Count == 0)
            range.Add(new (values.lb, values.ub));

        for (int idx = 0; idx < range.Count; idx++){
            (uint v1, uint v2) blRange = range[idx];

            // values completely contained in a range
            if (values.lb >= blRange.v1 && values.ub <= blRange.v2){
                return;
            }
            // values preppends a range
            else if (values.lb <= blRange.v1 && values.ub >= blRange.v1 - 1 && values.ub <= blRange.v2 ) {
                blRange.v1 = values.lb;
                range[idx] = blRange;
                return;
            }
            // values appends a range
            else if (values.lb >= blRange.v1 && values.lb <= blRange.v2 + 1 && values.ub >= blRange.v2) {
                blRange.v2 = values.ub;
                range[idx] = blRange;
                return;
            }
        }
        range.Add(new (values.lb, values.ub));
        return;
    }
}
