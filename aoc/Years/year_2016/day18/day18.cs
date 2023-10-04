using AdventOfCode.Runner.Attributes;

namespace Year2016.Day18;

[ProblemInfo(2016, 18, "Like a Rogue")]
public class Day18: Problem<int, int>{
    private List<string> board = new List<string>(400000);
    private int BoardSize = 0;

    public override void LoadInput() {
        board.Add(ReadInputText().Trim());
        BoardSize = board[0].Length;
        MakeBoard();
    }

    public override void CalculatePart1() {
        Part1 = board.Take(40).Sum(row => row.Count(v => v == '.')); 
    }
    public override void CalculatePart2() {
        Part2 = board.Sum(row => row.Count(v => v == '.')); 
    }

    private char GetTile(int pos, int line){
        var left = (pos-1) >= 0 ? board[line-1][pos-1] : '.';
        var center = board[line-1][pos];
        var right = (pos+1) < BoardSize ? board[line-1][pos+1] : '.';

        switch( (left,center,right) ){
            case ('^', '^', '.'):
                return '^';
            case ('.', '^', '^'):
                return '^';
            case ('^', '.', '.'):
                return '^';
            case ('.', '.', '^'):
                return '^';
            default:
                return '.';
        }
    }
    
    private void MakeBoard(){
        for ( int id = 1; id < 400000; id++){
            List<char> row = new List<char>(BoardSize);
            for (int idx = 0; idx < BoardSize; idx++){
                var v = GetTile(idx, id);
                row.Add(v);
            }
            board.Add(row.Stringify());
        }
    }

}
