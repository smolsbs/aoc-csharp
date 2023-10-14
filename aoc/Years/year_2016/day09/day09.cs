using System.Numerics;
using AdventOfCode.Runner.Attributes;

namespace Year2016.Day9;

[ProblemInfo(2016, 9, "Explosives in Cyberspace")]
public class Day9: Problem<int,BigInteger>{
    private string _inputData = "";

    public override void LoadInput() {
        _inputData = ReadInputText();

    }
    public override void CalculatePart1() {
        Part1 = Thing(_inputData);
        return;

    }
    public override void CalculatePart2() {
        // this will take around 8 mins or less, depending on your cpu
        // go make some coffee, take a shower, touch grass, idk
        // you do you

        Part2 = ThingV2(_inputData);
    }

    private int Thing (string line){
        int state = 0;
        int decompressed = 0;
        string aux = "";
        for (var i = 0; i < line.Length; i++){
            char c = line[i];
            switch (state){
                case 0:
                    if (c == '('){
                        aux = "";
                        state = 1;
                    } else
                        decompressed++;
                    break;

                case 1:
                    if (c == ')')
                        state = 2;
                    aux += c;
                    break;

                case 2:
                    aux = aux[0..^1];
                    var values = aux.Split('x').Select(v => int.Parse(v)).ToArray();
                    decompressed += values[0]*values[1];
                    i += values[0]-1;
                    state = 0;
                    break;
            }
        }
        return decompressed;
    }

    private BigInteger ThingV2 (string line){
        int state = 0;
        BigInteger decompressed = 0;
        string aux = "";
        for (var i = 0; i < line.Length; i++){
            char c = line[i];
            switch (state){
                case 0:
                    if (c == '('){
                        aux = "";
                        state = 1;
                    } else
                        decompressed++;
                    break;

                case 1:
                    if (c == ')')
                        state = 2;
                    aux += c;
                    break;

                case 2:
                    aux = aux[0..^1];
                    var values = aux.Split('x').Select(v => int.Parse(v)).ToArray();
                    var patt = line[i..(i+values[0])];
                    var toCheck = string.Join("", Enumerable.Repeat(patt, values[1]));
                    decompressed += ThingV2(toCheck);
                    i += values[0]-1;
                    state = 0;
                    break;

            }
        }

        return decompressed;
    }    
}
