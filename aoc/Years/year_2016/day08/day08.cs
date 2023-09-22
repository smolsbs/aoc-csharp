using AdventOfCode.Runner.Attributes;
using System.Data;
using System.Numerics;

namespace Year2016.Day8;

[ProblemInfo(2016, 8, "Two-Factor Authentication")]
public class Day8: Problem<int, string>{
    private const int COLS = 50;
    private const int ROWS = 6;
    private List<string> _inputData;
    
    private bool[,] screen = new bool[ROWS, COLS];
    
    public override void LoadInput() {
        _inputData = ReadInputLines().ToList();
        return;
    }
    public override void CalculatePart1() {
        foreach(var line in _inputData){
            ParseCommand(line);
        }
        Part1 = plsGibCount();
    }
    public override void CalculatePart2() {
        // lmao read the output from part 1
        // aint no way I'm OCR'ing this or making a DICT TABLE FOR THIS SHIT
        // ;)
        Console.Write('\n');
        PrintScreen();
        Part2 = "CFLELOYFCS";
    }

    private void TurnOnRectangle(int sizeX, int sizeY){
        
        for (var i = 0; i<sizeY; i++){
            for (var j = 0; j < sizeX; j++){
                screen[j,i] = true;
            }
        }
    }

    private void ShiftScreen(bool mode, int pos, int amt){
        // mode: true -> rows, false -> columns

        if (mode){ // row y=1 by 1, shifting right the values
            var aux = screen.GetRow(pos);
            for (var x = 0; x< COLS; x++){
                // y is fixed, x moves
                screen[pos,x] = aux[(x-amt).mod(COLS)];
            }
        }else{ // column x=2 by 3, shifting down the values
            var aux = screen.GetColumn(pos);
            for (var y = 0; y < ROWS; y++){
                // x is fixed, y moves
                screen[y, pos] = aux[(y-amt).mod(ROWS)];
            }
        }
    }


    private void ParseCommand(string line)
    {
        var cmd = line.Split(" ");

        if (cmd.Length == 2){ // rect 2x1

            var aux = cmd[1].Split('x').Select(v => int.Parse(v)).ToArray();
            TurnOnRectangle(aux[1], aux[0]);
        } else { // rotate row y=0 by 5
            int amount = int.Parse(cmd[4]);
            var aux = cmd[2].Split('=');

            // if true, operate on row, else operate on column
            bool rowColumn = aux[0] == "y";
            int position = int.Parse(aux[1]);
            ShiftScreen(rowColumn, position, amount);
        }


    }
    public void PrintScreen(){
        for (var y=0; y<ROWS; y++){
            for (var x = 0; x < COLS; x++){
                Console.Write($"{(screen[y,x] == true ? '#' : '.')}");
            }
            Console.Write('\n');
        }
        Console.Write("=======\n");
    }
    
    private void PrintLine(bool[] line){
        for (int i=0; i<line.Count(); i++){
            Console.Write($"{(line[i] == true ? '#' : '.' )}");
        }
    }

    private int plsGibCount (){
        int total = 0;
        for (int row = 0; row < ROWS; row++){
            total += screen.GetRow(row).Count(v => v == true);
        }
        return total;
    }

}