using AdventOfCode.Runner.Attributes;

namespace Year2016.Day12;

[ProblemInfo(2016, 12, "Leonardo's Monorail")]
public class Day12: Problem<int,int>{
    private string[] _inputData = Array.Empty<string>();

    public override void LoadInput() {
        _inputData = ReadInputLines();
    }

    public override void CalculatePart1() {
        Assembunny machine = Assembunny.Create(_inputData);
        machine.Run();

        Part1 = machine.GetRegister('a');
    }

    public override void CalculatePart2() {
        Assembunny machine = Assembunny.Create(_inputData);
        machine.SetRegister('c', 1);
        machine.Run();

        Part2 = machine.GetRegister('a');
    }
}

public class Assembunny{
    public enum funcs{
        Cpy,
        Inc,
        Dec,
        Jnz
    }

    public struct instruction{
        public funcs instr;
        public char regA;
        public char regB;
        public int a;
        public int b;
    }

    private Dictionary<char, int> registers = new Dictionary<char, int>() { {'a',0},{'b',0},{'c',0},{'d',0} };
    private int pointer = 0;
    private List<instruction> code = new List<instruction>();

    public void Run(){
        while (pointer < code.Count){
            instruction v = code[pointer];
            switch(v.instr){
                case funcs.Cpy:
                    Cpy(v);
                    break;
                case funcs.Inc:
                    Inc(v);
                    break;
                case funcs.Dec:
                    Dec(v);
                    break;
                case funcs.Jnz:
                    Jnz(v);
                    break;
            }
        }
    }

    private void Cpy(instruction cmd){
        if (cmd.a != 0){
            registers[cmd.regA] = cmd.a;
        }else{
            registers[cmd.regB] = registers[cmd.regA];
        }
        pointer++;
    } 

    private void Inc(instruction cmd){
        registers[cmd.regA]++;
        pointer++;
    }

    private void Dec(instruction cmd){
        registers[cmd.regA]--;
        pointer++;
    }

    private void Jnz(instruction cmd){
        if (cmd.b != 0){
            if (cmd.a != 0)
                pointer += cmd.b;
            else
                pointer++;
        } else {
            if (registers[cmd.regA] != 0)
                pointer += cmd.a;
            else
                pointer++;
        }
   }

    public int GetRegister(char reg){
        return registers[reg];
    }

    public static Assembunny Create(string[] instructs){
        Assembunny aux = new Assembunny();
        foreach(var line in instructs){
            instruction i = new instruction();
            char r1 = (char)0;
            char r2 = (char)0;
            int v1 = 0;
            int v2 = 0;
            var cmd = line.Split(" ");
            switch (cmd[0]){
                case "inc":
                    r1 = cmd[1].ToChar();
                    i = new instruction{instr = funcs.Inc, regA = r1};
                    break;

                case "dec":
                    r1 = cmd[1].ToChar();
                    i = new instruction{instr = funcs.Dec, regA = r1};
                    break;

                case "cpy":
                    if (int.TryParse(cmd[1], out v1)){
                        r1 = cmd[2].ToChar();
                    }else{
                        r1 = cmd[1].ToChar();
                        r2 = cmd[2].ToChar();
                    }
                    i = new instruction{instr= funcs.Cpy, regA = r1, regB = r2, a = v1};
                    break;

                case "jnz":
                    if (int.TryParse(cmd[1], out v1)){
                        v2 = int.Parse(cmd[2]);
                    }else{
                        v1 = int.Parse(cmd[2]);
                        r1 = cmd[1].ToChar();
                    }
                    i = new instruction{instr= funcs.Jnz, regA = r1, a = v1, b = v2};
                    break;
            }
            aux.code.Add(i);
        }
        return aux;
    }

    public void SetRegister(char reg, int value){
        registers[reg] = value;
    }
}
