using AdventOfCode.Runner.Attributes;

namespace Year2016.Day10;

[ProblemInfo(2016, 10, "Balance Bots")]
public class Day10: Problem<int, int>{

    private enum Exits{
        Bot,
        Out
    }

    private string[] _inputData = Array.Empty<string>();
    Queue<(int botId, Exits out1, int v1, Exits out2, int v2)> commands = new Queue<(int, Exits, int, Exits, int)>();
    private Dictionary<int, Bot> botList = new Dictionary<int, Bot>();
    private Dictionary<int, int> outList = new Dictionary<int, int>();

    public override void LoadInput() {
        _inputData = ReadInputLines();
        Parser();
    }

    private void AddToBot(int ID, int value){
        if (!botList.ContainsKey(ID))
            botList.Add(ID, new Bot(ID));
        botList[ID].TryAddValue(value);
    }

    public override void CalculatePart1() {
        Part1 = RunBots();

    }
    public override void CalculatePart2() {
        Part2 = RunBots(true);
    }

    private int RunBots(bool part2=false){
        bool run = true;
        int _ret = 0;
        while(run){
            var cmd =  commands.Dequeue();

            if(!botList.ContainsKey(cmd.botId)){
                commands.Enqueue(cmd);
                continue;
            }

            var bot = botList[cmd.botId];

            // part 1
            (int low, int high) states = bot.CheckStates();
            if (states == (17, 61) && !part2){
                run = false;
                _ret = cmd.botId;
            }

            // part 2
            if (outList.ContainsKey(0) && outList.ContainsKey(1) && outList.ContainsKey(2) && part2){
                _ret =  outList[0] * outList[1] * outList[2];
                run = false;
        }

            if (states.low == -1 || states.high == -1){
                commands.Enqueue(cmd);
                continue;
            }

            if (cmd.out1 == Exits.Bot)
                AddToBot(cmd.v1, states.low);
            else{
                outList.TryAdd(cmd.v1, states.low);
            }

            if (cmd.out2 == Exits.Bot)
                AddToBot(cmd.v2, states.high);
            else{
                outList.TryAdd(cmd.v2, states.high);
            }
            botList[cmd.botId].Reset();
        }
        return _ret;
    }



    private void Parser(){
        foreach (var line in _inputData){
            var _words = line.Split(" ");

            if (_words.Length == 6){
                int botId = int.Parse(_words[^1]);
                int value = int.Parse(_words[1]);

                if (botList.ContainsKey(botId))
                    botList[botId].TryAddValue(value);
                else{
                    botList.Add(botId, new Bot(botId));
                    botList[botId].TryAddValue(value);
                }

            }else{
                int botGiver = int.Parse(_words[1]);
                int v1 = int.Parse(_words[6]);
                int v2 = int.Parse(_words[11]);

                Exits _out1 = _words[5] == "bot" ? Exits.Bot : Exits.Out;
                Exits _out2 = _words[10] == "bot" ? Exits.Bot : Exits.Out;
                commands.Enqueue((botGiver, _out1, v1, _out2, v2));
            }
        }
    }
}


public class Bot{
    public int ID;
    private int? lowState = null;
    private int? highState = null;

    public Bot (int id) {ID = id;}

    public bool TryAddValue(int value){
        if (lowState != null && highState != null)
            return false;

        if (highState == null){
            highState = value;
            return true;
        } else if (value > highState){

            lowState = highState;
            highState = value;
        }else{
            lowState = value;
        }
        return true;
    }

    public (int low, int high) CheckStates(){
        return (lowState ?? -1 , highState ?? -1);
    }

    public void Reset(){
        lowState = null;
        highState = null;
    }
}

