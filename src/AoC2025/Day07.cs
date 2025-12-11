using Common;

namespace AoC2025;

public class Day07 : BaseLibraryDay
{
    protected override int Year => 2025;
    private string[] _input;

    public Day07()
    {
        _input = File.ReadAllLines(InputFilePath);
    }

    public override ValueTask<string> Solve_1()
    {


        return new(BeamDown(0, 70, new HashSet<(int, int)>()).ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        return new(CountTimelines(0, 70, new Dictionary<(int, int), long>()).ToString());
    }

    private int BeamDown(int i, int j, HashSet<(int, int)> visited)
    {
        if (j < 0 || j >= _input.Length) return 0;

        while (i < _input.Length)
        {
            if (_input[i][j] == '^')
            {
                if (visited.Contains((i, j))) return 0;
                visited.Add((i, j));
                return 1 + BeamDown(i, j - 1, visited) + BeamDown(i, j + 1, visited);
            }
            i++;
        }
        return 0;
    }

    private long CountTimelines(int startPosition, int j, Dictionary<(int, int), long> possibleTimelines)
    {
        int i = startPosition;
        if (j < 0 || j >= _input.Length) return 0;
        while (_input[i][j] != '^')
        {
            i++;
            if (i >= _input.Length) return 1;
        }

        if(possibleTimelines.ContainsKey((startPosition, j)))
        {
            return possibleTimelines[(startPosition, j)];
        }
        
        var left = CountTimelines(i, j - 1, possibleTimelines);
        var right = CountTimelines(i, j + 1, possibleTimelines);
        possibleTimelines.Add((startPosition, j), left + right);
        return left + right;    
    }
}
