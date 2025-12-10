using Common;

namespace AoC2025;

public record Point(int X, int Y, int Z);

public class Day08 : BaseLibraryDay
{
    protected override int Year => 2025;
    private string[] _input;
    private Dictionary<Point, int> points;
    
    public Day08()
    {
        _input = File.ReadAllLines(InputFilePath);
        ParseInput();
    }
   
    public void ParseInput()
    {
        points = _input.Select((line, position) =>
        {
            var coords = line.Split(',').Select(int.Parse).ToArray();
            return new { Key = new Point(coords[0], coords[1], coords[2]), Value = position };
        }).ToDictionary(x => x.Key, x=> x.Value);
    }

    public override ValueTask<string> Solve_1()
    {
        var query = points.Keys.SelectMany((value, index) => points.Keys.Skip(index + 1), (first, second) => new { first, second }).OrderBy(x => GetDistanceDiff(x.first, x.second));
        var union = new UnionFind(points.Count);
        foreach(var x in query.Take(1000))
        {
            union.Union(points[x.first], points[x.second]);
        }

        return new(union.MultiplySizesOfSets(3).ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        var query = points.Keys.SelectMany((value, index) => points.Keys.Skip(index + 1), (first, second) => new { first, second }).OrderBy(x => GetDistanceDiff(x.first, x.second));
        var union = new UnionFind(points.Count);
        foreach (var x in query)
        {
            if (union.UnionWithFullConnectCheck(points[x.first], points[x.second])) 
                return new(((long)x.first.X * x.second.X).ToString());
        }
        return new();
    }

    private static double GetDistanceDiff(Point a, Point b)
    {
        return Math.Sqrt(Math.Pow(a.X - b.X, 2) + Math.Pow(a.Y - b.Y, 2) + Math.Pow(a.Z - b.Z, 2));
    }
}

public class UnionFind
{
    int[] parent;
    int[] size;
    public UnionFind(int n)
    {
        parent = Enumerable.Range(0, n).ToArray();
        size = Enumerable.Repeat(1, n).ToArray();
    }

    public int Find(int x)
    {
        if (parent[x] != x)
        {
            parent[x] = Find(parent[x]);
            
        }
        return parent[x];
    }

    public void Union(int x, int y)
    {
        var rootX = Find(x);
        var rootY = Find(y);

        if (rootX == rootY) return;

        if (size[rootX] > size[rootY])
        {
            parent[rootY] = rootX;
            size[rootX] += size[rootY];
        }
        else
        {
            parent[rootX] = rootY;
            size[rootY] += size[rootX];
        }
    }

    public bool UnionWithFullConnectCheck(int x, int y)
    {
        var rootX = Find(x);
        var rootY = Find(y);

        if (rootX == rootY) return false;

        if (size[rootX] > size[rootY])
        {
            parent[rootY] = rootX;
            size[rootX] += size[rootY];
            if (size[rootX] >= parent.Length) return true;

        }
        else
        {
            parent[rootX] = rootY;
            size[rootY] += size[rootX];
            if (size[rootY] >= parent.Length) return true;
        }
        return false;
    }
    public long MultiplySizesOfSets(int n) => size.OrderDescending().Take(n).Aggregate((long)1, (a, b) => a * b);
}