namespace Assembunny;


public struct Instruction{
    public Funcs instr;
    public List<(string type, int value)> args;

    public override string ToString()
        {
            var _ret = new StringBuilder();
            _ret.Append($"{instr}");
            foreach(var v in args){
                _ret.Append($" {v}");
            }
            return _ret.ToString();
        }

    public Instruction (Funcs i, List<(string, int)> arg){ instr = i; args = arg;}
}

public enum Funcs{
    Cpy,
    Inc,
    Dec,
    Jnz,
    Tgl,
    Add,
    Mul
}
public class Assembunny{

    private Dictionary<char, int> registers = new Dictionary<char, int>() { {'a',0},{'b',0},{'c',0},{'d',0} };
    private int pointer = 0;
    private List<Instruction> code = new List<Instruction>();

    public void Run(bool p2=false){
        while (pointer < code.Count){

            var thing = GetOptimizedCode();
            Instruction v = thing.Item1 ?? code[pointer];
             if (p2){
                Console.WriteLine(v.instr);
                for (var idx = 0; idx < code.Count; idx++){
                   Console.WriteLine($"{(idx == pointer ? "->" : "  ")}{code[idx].ToString()}");
                }
            }

            switch(v.instr){
                case Funcs.Cpy:
                    Cpy(v);
                    break;
                case Funcs.Inc:
                    Inc(v);
                    break;
                case Funcs.Dec:
                    Dec(v);
                    break;
                case Funcs.Jnz:
                    Jnz(v);
                    break;
                case Funcs.Tgl:
                    Tgl(v);
                    break;
                case Funcs.Add:
                    Add(v, thing.Item2);
                    break;
                case Funcs.Mul:
                    Mul(v, thing.Item2);
                    break;
            }
            if (p2){
                Console.WriteLine(string.Join(" ", registers.Select( i => $"{i.Key}: {i.Value}").ToArray()));
                var _ = Console.Read();
            }
        }
    }

    private (Instruction?, int) GetOptimizedCode(){
        Instruction curr = code[pointer];

        if (curr.instr != Funcs.Inc)
            return (null, 0);
        
        if (code[pointer+1].instr != Funcs.Dec || code[pointer+2].instr !!= Funcs.Jnz)
            return (null, 0);
        
        if (code[pointer+1].args[0].value != code[pointer+2].args[0].value)
            return (null, 0);

        int steps = 3;
        char a = (char)code[pointer+1].args[0].value;
        List<(string, int)> newArgs = new List<(string, int)>() {(" ", (int)a)};

        if (code[pointer+3].instr != Funcs.Dec || code[pointer+4].instr !!= Funcs.Jnz)
            return (new Instruction(Funcs.Add, newArgs), steps);
        
        if (code[pointer+3].args[0].value != code[pointer+4].args[0].value)
            return (new Instruction(Funcs.Add, newArgs), steps);

        steps += 2;
        char b = (char)code[pointer+3].args[0].value;
        newArgs.Add( (" ", (int)b));

        return (new Instruction(Funcs.Mul, newArgs), steps);

    }

    private void Add(Instruction cmd, int steps){
        var arg1 = cmd.args[0];

        registers['a'] += registers[(char)arg1.value];
        registers[(char)arg1.value] = 0;
        pointer += steps;

    }


    private void Mul(Instruction cmd, int steps){
        var arg1 = cmd.args[0];
        var arg2 = cmd.args[1];

        registers['a'] += registers[(char)arg1.value] * registers[(char)arg2.value];
        registers[(char)arg1.value] = 0;
        registers[(char)arg2.value] = 0;
        pointer += steps;
    }


    private void Cpy(Instruction cmd){
        if (cmd.args[0].type == "a" && cmd.args[1].type == "b"){
            pointer++;
            return;
        }

        var arg1 = cmd.args[0];
        var arg2 = cmd.args[1];

        if(arg2.type == "regB")
            registers[(char)arg2.value] = registers[(char)arg1.value];
        else
            registers[(char)arg2.value] = arg1.value;

        pointer++;
    } 

    private void Inc(Instruction cmd){
        registers[(char)cmd.args[0].value]++;
        pointer++;
    }

    private void Dec(Instruction cmd){
        registers[(char)cmd.args[0].value]--;
        pointer++;
    }

    private void Jnz(Instruction cmd){
        var arg1 = cmd.args[0];
        var arg2 = cmd.args[1];
        
        if (arg1.type == "regA"){
            char p = (char)arg1.value;
            if (arg2.type == "a")
                pointer += registers[p] != 0 ? arg2.value : 1;
            else
                pointer += registers[(char)arg1.value] != 0 ? registers[(char)arg2.value] : 1;
        }else if (arg1.type == "a"){
            if (arg2.type == "b")
                pointer += arg1.value != 0 ? arg2.value : 1;
            else
                pointer += arg1.value != 0 ? registers[(char)arg2.value] : 1;
        }
   }

    private void Tgl(Instruction cmd){
        int jmp;
        var arg1 = cmd.args[0];
        if (arg1.type == "a")
            jmp = arg1.value;
        else
            jmp = registers[(char)arg1.value];

        if (pointer+jmp < 0 || pointer+jmp >= code.Count){
            pointer++;
            return;
        }

        Instruction toChange = code[pointer+jmp];

        Funcs newFunc;

        if (toChange.args.Count == 1){
            if (toChange.instr == Funcs.Inc)
                newFunc = Funcs.Dec;
            else
                newFunc = Funcs.Inc;
        }else{
            if (toChange.instr == Funcs.Jnz)
                newFunc = Funcs.Cpy;
            else
                newFunc = Funcs.Jnz;
        }
        code[pointer+jmp] = new Instruction {
            instr = newFunc, 
            args = toChange.args
        };
        pointer++;
    }
    
    public int GetRegister(char reg){
        return registers[reg];
    }


    public static Assembunny Create(string[] instructs){
        Assembunny aux = new Assembunny();
        foreach(var line in instructs){
            Instruction i = new Instruction();
            List<(string type, int value)> a = new List<(string,int)>();
            int v1;
            var cmd = line.Split(" ");
            switch (cmd[0]){
                case "inc":
                    a.Add(("regA", (int)cmd[1].ToChar()));
                    i = new Instruction{instr = Funcs.Inc, args = a };
                    break;

                case "dec":
                    a.Add(("regA", (int)cmd[1].ToChar()));
                    i = new Instruction{instr = Funcs.Dec, args = a};
                    break;

                case "cpy":
                    if (int.TryParse(cmd[1], out v1)){
                        a.Add(("a", v1));
                        a.Add(("regA", (int)cmd[2].ToChar()));
                    }else{
                        a.Add(("regA", (int)cmd[1].ToChar()));
                        a.Add(("regB", (int)cmd[2].ToChar()));
                    }
                    i = new Instruction{instr= Funcs.Cpy, args = a};
                    break;

                case "jnz":
                    // first arg can be value a or regA
                    if(int.TryParse(cmd[1], out v1))
                        a.Add(("a", v1));
                    else
                        a.Add(("regA", (int)cmd[1].ToChar()));

                    // second arg can be value b, regA or regB if arg1 is regA
                    if(int.TryParse(cmd[2], out v1)){
                        if (a[0].type == "a")
                            a.Add(("b", v1));
                        else if (a[0].type == "regA")
                            a.Add(("a", v1));
                    }else{
                        if (a[0].type == "a")
                            a.Add(("regA", (int)cmd[2].ToChar()));
                        else
                            a.Add(("regB", (int)cmd[2].ToChar()));
                    }

                    i = new Instruction{instr= Funcs.Jnz, args = a};
                    break;

                case "tgl":
                    int c;
                    if (int.TryParse(cmd[1], out c))
                        a.Add(("a",c));
                    else
                        a.Add(("regA", (int)cmd[1].ToChar()));

                    i = new Instruction{instr=Funcs.Tgl, args=a};
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
