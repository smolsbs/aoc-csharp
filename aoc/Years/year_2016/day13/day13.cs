using AdventOfCode.Runner.Attributes;
using System.Drawing;

namespace Year2016.Day13;

[ProblemInfo(2016, 13, "A Maze of Twisty Little Cubicles")]
public class Day13: Problem<int,int>{
    private Point endPos = new Point(31,39);
    private int _inputData;

    public override void LoadInput() {
        _inputData = int.Parse(ReadInputText());
    }
    
    private class Node{
        public Point coord;
        public Point parent;
        public int f,g,h;
    }

    public int AStar(){
        List<Node> openList = new List<Node>();
        Dictionary<Point, Node> closedList = new Dictionary<Point, Node>();
        openList.Add( new Node{coord = new Point(1,1), g = 0, f = 0} );

        while ( openList.Count != 0) {
            Node currNode = openList.OrderBy(v => v.f).First();
            openList.RemoveAll( n => n.coord == currNode.coord);
            if (currNode.coord == endPos){
                return currNode.g;
            }
            closedList.Add(currNode.coord, currNode);

            foreach( var child in GetNeighbours(currNode.coord) ){
                if (closedList.Any( v => v.Key == child )){
                    continue;
                }

                int child_g = currNode.g + 1;
                int child_h = GetH(child);
                int child_f = child_g + child_h;

                var aux = openList.FirstOrDefault(v => v.coord == child);

                if (aux != null && child_g >= aux.g ) {
                    continue;
                } else if ( aux != null  && child_g < aux.g ){
                    if (closedList.ContainsKey(child)){
                        var aux2 =  closedList[child];
                        if (aux2.f < child_f)
                            continue;
                    }
                    aux.g = child_g;
                    aux.h = child_h;
                    aux.f = child_f;
                    continue;
                }
                openList.Add( new Node { coord = child, parent= currNode.coord, f = child_f, g = child_g, h = child_h });
            }
        }
        return 0;
    }

    public List<Point> GetNeighbours(Point parent){
        List<Point> ret = new List<Point>();
        (int x, int y)[] validMoves = new []{ (-1,0), (1,0), (0,-1), (0,1) };

        foreach( var move in validMoves) {
            Point newPoint = new Point( parent.X + move.x, parent.Y + move.y );
            
            if (newPoint.X < 0 || newPoint.Y < 0)
                continue;
            if (CalculateGrid(newPoint) == 0)
                ret.Add(newPoint);
        }
        return ret;
    }

    private int GetH(Point point){
        return (int)Math.Sqrt( Math.Pow(endPos.X - point.X, 2) + Math.Pow(endPos.Y - point.Y, 2));
    }

    public override void CalculatePart1(){
        Part1 = AStar();
    }

    private Dictionary<Point, int> seen = new Dictionary<Point, int>();
    private void BFS(Point point, int steps=0){
        if (!seen.TryAdd(point, steps)){
            if (seen[point] > steps)
                seen[point] = steps;
            else
                return;
        }
        if (steps > 50)
            return;

        var children = GetNeighbours(point);
        foreach( var child in children){
            BFS(child, steps+1);
        }
    }

    public override void CalculatePart2(){
        BFS(new Point(1,1), 0);
        var aux = seen.Count(v => v.Value <= 50);

    Part2 = aux;
    }

    private int CalculateGrid(Point point){
        int x = point.X;
        int y = point.Y;
        var equation = x*x + 3*x + 2*x*y + y + y*y;
        equation += _inputData;
        return SumBitOnes(equation) % 2; 
    }

    private int SumBitOnes(int value){
        int count;
        for (count = 0; value != 0; count++, value &= value-1){};
        return count;
    }
}
