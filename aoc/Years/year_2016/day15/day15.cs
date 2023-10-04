using AdventOfCode.Runner.Attributes;

namespace Year2016.Day15;

[ProblemInfo(2016, 15, "Timing is Everything")]
public class Day15: Problem<int,int>{
    private List<string> _inputData = new List<string>();
    private List<Disc> beybladeHolder = new List<Disc>();

    public override void LoadInput() {
        _inputData = ReadInputLines().ToList();
        var regex = new Regex(@"Disc #(?<disc_id>\d+) has (?<n_pos>\d+) positions; at time=(?<time>\d), it is at position (?<pos>\d+).");
        foreach(var line in _inputData){
            var thing = regex.Match(line);
            var disc = new Disc(int.Parse(thing.Groups["disc_id"].Value),
                    int.Parse(thing.Groups["n_pos"].Value),
                    int.Parse(thing.Groups["pos"].Value));

            beybladeHolder.Add(disc);
        }
    }

    public override void CalculatePart1() {
        Part1 = LetItRip();
    }

    public override void CalculatePart2() {
        beybladeHolder.Clear();
        LoadInput();
        beybladeHolder.Add(new Disc(7, 11, 0));

        Part2 = LetItRip();
    }

    private int LetItRip(){
        int idx = 0;
        while(true){
            foreach( Disc d in beybladeHolder){
                d.Step();
            }
            
            if (beybladeHolder.All(d => d.Offset(d.ID-1) == 0))
                return idx;
            idx++;
        }
    }
}

public class Disc{
    public int posLen;
    public int currPos;
    public int ID;

    public Disc(int id, int posLen, int currPos){
        this.ID = id; 
        this.currPos = currPos; 
        this.posLen = posLen;
    }

    public int Offset(int offset){
        return (currPos+offset).mod(posLen);
    }

    public void Step(){
        currPos = (currPos+1).mod(posLen);
    }

}
