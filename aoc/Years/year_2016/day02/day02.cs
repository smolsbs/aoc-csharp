using AdventOfCode.Runner.Attributes;

namespace Year2016.Day02;

[ProblemInfo(2016, 2, "Bathroom Security")]
public class Day02 : Problem{

    private string[] _inputData = Array.Empty<string>();

    public override void LoadInput() {
        _inputData = ReadInputLines();
    }
    public override void CalculatePart1() {
        var keypad = new Keypad();
        Part1 = "";
        foreach(var line in _inputData){
            foreach(char c in line){
                keypad.Move(c);
            }
            Part1 += keypad.GetDigit();
        }
        return ;
    }
    public override void CalculatePart2() {
        var keypad = new FancyKeypad();
        Part2 = "";
        foreach(var line in _inputData){
            foreach(char c in line){
                keypad.Move(c);
            }
            Part2 += keypad.GetDigit();
        }
        return;
    }
}
internal class Keypad{
    int X = 1;
    int Y = 1;
    public void Move(char c){
        switch(c){
            case 'U':
                if(Y > 0 && Y < 3) Y--;
                break;
            case 'D':
                if(Y > -1 && Y < 2) Y++;
                break;
            case 'L':
                if(X > 0 && X < 3) X--;
                break;
            case 'R':
                if(X > -1 && X < 2) X++;
                break;
        }
    }
    public string GetDigit(){
        return $"{(Y * 3) + X + 1}";
    }
}

internal class FancyKeypad{
    int X = 0;
    int Y = 2;
    Dictionary<(int, int), char> _keypad = new Dictionary<(int, int), char>(){{(2, 0), '1'},{(1, 1), '2'}, {(2, 1), '3'}, {(3, 1), '4'},{(0, 2), '5'}, {(1, 2), '6'}, {(2, 2), '7'}, {(3, 2), '8'}, {(4, 2), '9'},{(1, 3), 'A'}, {(2, 3), 'B'}, {(3, 3), 'C'},{(2, 4), 'D'}};

    public void Move(char c){
        switch(c){
            case 'U':
                if(_keypad.ContainsKey((X, Y - 1))) Y--;
                break;
            case 'D':
                if(_keypad.ContainsKey((X, Y + 1))) Y++;
                break;
            case 'L':
                if(_keypad.ContainsKey((X - 1, Y))) X--;
                break;
            case 'R':
                if(_keypad.ContainsKey((X + 1, Y))) X++;
                break;
        }
    }
    
    public char GetDigit(){
        return _keypad[(X, Y)];
    }
}