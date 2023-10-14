using AdventOfCode.Runner.Attributes;
using System.Drawing;
using System.Collections.Concurrent;

namespace Year2016.Day24;

[ProblemInfo(2016, 24, "Air Duct Spelunking")]
public class Day24: Problem<int, int>{

    private List<List<int>> grid = new List<List<int>>();
    Dictionary<int, Point> points = new Dictionary<int, Point>();

    private int MaxX = 0;
    private int MaxY = 0;

    public override void LoadInput() {
        var _input = ReadInputLines();
        MaxY = _input.Length;
        MaxX = _input[0].Length;

        for (int i = 0; i< MaxY; i++){
            List<int> row = new List<int>(MaxX);
            for (int j = 0; j < MaxX; j++){
                switch(_input[i][j]){
                    case '.':
                        row.Add(0);
                        break;
                    case '#':
                        row.Add(1);
                        break;
                    default:
                        points[int.Parse(_input[i][j].ToString())] = new Point(j, i);
                        row.Add(0);
                        break;
                }
            }
            grid.Add(row);
        }
        CalculateAStars();
    }

    List<(int a, int b)> poi = new List<(int, int)>();
    ConcurrentDictionary<(Point, Point), int> steps = new ConcurrentDictionary<(Point, Point), int>();

    private void CalculateAStars(){
        for (int i = 0; i < points.Count-1; i++){
            for (int j = i+1; j < points.Count; j++){
                poi.Add((i,j));
            }
        }

        Parallel.For(0, poi.Count, (idx, _) => 
                {

                Point start = points[poi[idx].a];
                Point end = points[poi[idx].b];
                int cost = AStar(start, end);

                steps[(start, end)] = cost;
                steps[(end, start)] = cost;
        });
    }

    public override void CalculatePart1() {
        var permutations = Enumerable.Range(1, points.Count-1).ToArray().Permute();
        int best = int.MaxValue;

        foreach(var p in permutations){
            int aux = steps[(points[0], points[p[0]])];

            for (int idx = 0; idx < p.Count-1; idx++){
                aux += steps[(points[p[idx]], points[p[idx+1]])];

                if ( aux > best)
                    break;
            }

            best = aux < best ? aux : best;

        }
        Part1 = best;

    }

    public override void CalculatePart2() {
        var permutations = Enumerable.Range(1, points.Count-1).ToArray().Permute();
        int best = int.MaxValue;

        foreach(var p in permutations){
            int aux = steps[(points[0], points[p[0]])];

            for (int idx = 0; idx < p.Count-1; idx++){
                aux += steps[(points[p[idx]], points[p[idx+1]])];

                if ( aux > best)
                    break;
            }

            aux += steps[(points[p[^1]], points[0])];

            best = aux < best ? aux : best;

        }
        Part2 = best;
    }

    private int Combinations(int n, int r){
        int nFactorial = Enumerable.Range(0, n).Aggregate(1, (p, item) => p *item);
        int rFactorial = Enumerable.Range(0, r).Aggregate(1, (p, item) => p *item);
        int nrFactorial = Enumerable.Range(0, (n-r)).Aggregate(1, (p, item) => p *item);

        return nFactorial / (rFactorial * nrFactorial);
    }


    private class Node{
        public Point coord;
        public Point parent;
        public int f,g,h;
    }

    public int AStar(Point startPos, Point endPos){
        List<Node> openList = new List<Node>();
        Dictionary<Point, Node> closedList = new Dictionary<Point, Node>();
        openList.Add( new Node{coord = startPos, g = 0, f = 0} );

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
        return 0;
    }

    public List<Point> GetNeighbours(Point parent){
        List<Point> ret = new List<Point>();
        (int x, int y)[] validMoves = new []{ (-1,0), (1,0), (0,-1), (0,1) };

        foreach( var move in validMoves) {
            Point newPoint = new Point( parent.X + move.x, parent.Y + move.y );
            
            if (newPoint.X < 0 || newPoint.Y < 0 || newPoint.X > MaxX - 1 || newPoint.Y > MaxY - 1 )
                continue;
            if (grid[newPoint.Y][newPoint.X] == 0)
                ret.Add(newPoint);
        }
        return ret;
    }

    private int GetH(Point point, Point endPos){
        return (int)Math.Sqrt( Math.Pow(endPos.X - point.X, 2) + Math.Pow(endPos.Y - point.Y, 2));
    }
}
