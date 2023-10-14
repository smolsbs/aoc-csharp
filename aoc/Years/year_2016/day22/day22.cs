using AdventOfCode.Runner.Attributes;
using System.Drawing;

namespace Year2016.Day22;

[ProblemInfo(2016, 22, "Grid Computing")]
public class Day22: Problem<int,int>{
    private List<NodeThing> nodes = new List<NodeThing>(); 

    public override void LoadInput() {
        Regex finder = new Regex(@"x(?<x>\d+)-y(?<y>\d+)\s+(?<size>\d+)T\s+(?<used>\d+)T\s+(?<avail>\d+)T\s+(?<perc>\d+)%");
        foreach( var line in ReadInputLines() ){
            var m = finder.Match(line);
            if (m.Success){
                var entry = new NodeThing{
                    x=int.Parse(m.Groups["x"].Value),
                    y=int.Parse(m.Groups["y"].Value),
                    size=int.Parse(m.Groups["size"].Value),
                    used=int.Parse(m.Groups["used"].Value),
                    avail=int.Parse(m.Groups["avail"].Value),
                    percent=int.Parse(m.Groups["perc"].Value),
                };
            nodes.Add(entry);
            }
        }
    }

    public override void CalculatePart1() {
        for (int i = 0; i < nodes.Count; i++){
            for (int j = i+1; j < nodes.Count; j++){
                if (nodes[i].used != 0 && nodes[j].avail >= nodes[i].used)
                    Part1++;
                if (nodes[j].used != 0 && nodes[i].avail >= nodes[j].used)
                    Part1++;
            }
        }
    }

    List<List<int>> grid = new List<List<int>>();
    private int max_y = 0;
    private int max_x = 0;

    private void MakeGrid(){
        max_y = nodes.OrderByDescending(v => v.y).First().y;
        max_x = nodes.OrderByDescending(v => v.x).First().x;
        for (int i = 0; i <= max_y; i++){
            var row = nodes.Where(v => v.y == i).OrderBy(v => v.x).ToList();
            List<int> line = new List<int>();
            for (int j = 0; j <= max_x; j++){
                // 1 -> normal
                // 0 -> empty spot
                // 2 -> wall
                // 3 -> Goal
                int c = 1;
                if ( i == 0 && j == max_x)
                    c = 3;
                else if ( row[j].used > 150)
                    c = 2;
                else if ( row[j].used == 0)
                    c = 0;
                line.Add(c);
            }
            grid.Add(line);
        }
    }

    public override void CalculatePart2() {
        MakeGrid();
        int steps = 0;
        Point target = new Point(max_x-1, 0); 
        var aux = nodes.Where(v => v.used == 0).First();
        Point empty = new Point( aux.x, aux.y );

        Node a = AStar(empty, target)!;
        steps = a.g;

        grid[empty.Y][empty.X] = 1;
        empty = a.coord;

        while(true){
            grid[empty.Y][empty.X+1] = 0;
            grid[empty.Y][empty.X] = 3;
            steps++;

            if (grid[0][0] == 3)
                break;

            empty = new Point(empty.X -1, empty.Y);
            Point _s = new Point(empty.X+2, empty.Y);

            var b = AStar(_s, empty);
            if (b != null)
                steps += b.g;
        }
        Part2 = steps;
    }

    private class Node{
        public Point coord;
        public Point parent;
        public int f,g,h;
    }

    private void PrintGrid(){
        foreach(var row in grid){
            Console.WriteLine(row.Select(v => v.ToString()).ToList().JoinStrings() );
        }
        Console.WriteLine();
    }
    
    private Node? AStar(Point startPos, Point endPos){
        List<Node> openList = new List<Node>();
        Dictionary<Point, Node> closedList = new Dictionary<Point, Node>();
        openList.Add( new Node{coord = startPos, g = 0, f = 0} );

        while ( openList.Count != 0) {
            Node currNode = openList.OrderBy(v => v.f).First();
            openList.RemoveAll( n => n.coord == currNode.coord);
            if (currNode.coord == endPos)
                return currNode;

            closedList.Add(currNode.coord, currNode);

            foreach( var child in GetNeighbours(currNode.coord) ){
                if (closedList.Any( v => v.Key == child )){
                    continue;
                }

                int child_g = currNode.g + 1;
                int child_h = GetH(child, endPos);
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
        return null;
    }

    public List<Point> GetNeighbours(Point parent ){
        List<Point> ret = new List<Point>();
        (int x, int y)[] validMoves = new []{ (-1,0), (1,0), (0,-1), (0,1) };

        foreach( var move in validMoves) {
            Point newPoint = new Point( parent.X + move.x, parent.Y + move.y );
            if (newPoint.X < 0 || newPoint.X > max_x || newPoint.Y < 0 || newPoint.Y > max_y)
                continue;
            if (grid[newPoint.Y][newPoint.X] == 1)
                ret.Add(newPoint);
        }
        return ret;
    }

    private int GetH(Point point, Point endPos){
        //return (int)Math.Sqrt( Math.Pow(endPos.X - point.X, 2) + Math.Pow(endPos.Y - point.Y, 2));
        return Math.Abs(endPos.X - point.X) + Math.Abs(endPos.Y - point.Y);
    }
}

public struct NodeThing{
    public int x;
    public int y;
    public int size;
    public int used;
    public int avail;
    public int percent;
}
