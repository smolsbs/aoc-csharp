namespace Year2016.Day02;

private class Keypad{
    int X = 1;
    int Y = 1;
    // 1 2 3
    // 4 5 6
    // 7 6 9
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

public static class Day02{
    public static void Run(string Path){
        var input = File.ReadAllLines(Path);
        var keypad = new Keypad();
        var code = "";
        foreach(var line in input){
            foreach(var c in line){
                keypad.Move(c);
            }
            code += keypad.GetDigit();
        }
        Console.WriteLine(code);
    }
}