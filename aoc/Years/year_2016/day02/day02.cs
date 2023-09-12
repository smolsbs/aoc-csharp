using AdventOfCode.Runner.Attributes;

namespace Year2016.Day02;

[ProblemInfo(2016, 2, "Day 2: Bathroom Security")]
public class DayTemplate : Problem{

    private string[] _inputData = new string[0];

    public override void LoadInput() {
        _inputData = ReadInputLines();
    }
    public override void CalculatePart1() {
        var keypad = new Keypad();
        var Part1 = "";
        foreach(var line in _inputData){
            foreach(var c in line){
                keypad.Move(c);
            }
            Part1 += keypad.GetDigit();
        }
        return ;
    }
    public override void CalculatePart2() {
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

