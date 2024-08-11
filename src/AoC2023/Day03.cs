using System.Text;
using Common;

namespace AoC_2023;

public class Day03 : BaseLibraryDay
{
    private readonly string[] _input;
    protected override int Year => 2023;

    public Day03()
    {
        _input = File.ReadAllLines(InputFilePath);
    }

    private bool IsSymbol(int i, int j)
    {
        return !char.IsDigit(_input[i][j]) && _input[i][j] != '.';
    }

    private IEnumerable<Tuple<int, int>> GetAdjacent(int i, int j)
    {
        int height = _input.Length;
        int width = _input[0].Length;
        if (i > 0 && j > 0) yield return new(i - 1, j - 1); //left up
        if (i > 0) yield return new(i - 1, j); //up 
        if (i > 0 && j < (width - 1)) yield return new(i - 1, j + 1); // right up
        if (j < (width - 1)) yield return new(i, j + 1); //right
        if (i < (height - 1) && j < (width - 1)) yield return new(i + 1, j + 1); //right bottom
        if (i < (height - 1)) yield return new(i + 1, j); // bottom
        if (i < (height - 1) && j > 0) yield return new(i + 1, j - 1); // left bottom
        if (j > 0) yield return new(i, j - 1); //left
    }

    private int GetPartValue(int i, int j)
    {
        var width = _input[0].Length;
        var value = "";
        var numberIndexes = new List<int>();
        while (j < width && char.IsDigit(_input[i][j]))
        {
            numberIndexes.Add(j);
            value += _input[i][j];
            j++;
        }

        return numberIndexes
            .Any(index => GetAdjacent(i, index)
                .Any(adj => IsSymbol(adj.Item1, adj.Item2)))
            ? int.Parse(value)
            : 0;
    }

    public override ValueTask<string> Solve_1()
    {
        var sum = 0;
        for (var i = 0; i < _input.Length; i++)
        {
            for (int j = 0; j < _input[i].Length; j++)
            {
                if (char.IsDigit(_input[i][j]))
                {
                    var currentValue = GetPartValue(i, j);
                    sum += currentValue;
                    if (currentValue != 0) j += (int)Math.Floor(Math.Log10(currentValue));
                }
            }
        }

        return new(sum.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        var sum = 0;
        var visitedGrid = new bool[_input.Length, _input[0].Length];
        for (int i = 0; i < _input.Length; i++)
        {
            for (int j = 0; j < _input[i].Length; j++)
            {
                if (_input[i][j] == '*')
                {
                    sum += GetGearRatio(i, j, visitedGrid);
                }
            }
        }

        return new(sum.ToString());
    }

    private int GetGearRatio(int i, int j, bool[,] visitedGrid)
    {
        var counter = 0;
        var ratio = 1;
        foreach (var adjacent in GetAdjacent(i, j))
        {
            if (char.IsDigit(_input[adjacent.Item1][adjacent.Item2]) && !visitedGrid[adjacent.Item1, adjacent.Item2])
            {
                var n = GetGearPartNumber(adjacent.Item1, adjacent.Item2, visitedGrid);
                ratio *= n;
                counter++;
            }
        }

        return counter == 2 ? ratio : 0;
    }

    private int GetGearPartNumber(int i, int j, bool[,] visitedGrid)
    {
        var builder = new StringBuilder();
        builder.Append(_input[i][j]);
        visitedGrid[i, j] = true;
        for (int prevIndex = j - 1; prevIndex >= 0; prevIndex--)
        {
            if (!char.IsDigit(_input[i][prevIndex]) || visitedGrid[i, prevIndex]) break;
            visitedGrid[i, prevIndex] = true;
            builder.Insert(0, _input[i][prevIndex]);
        }

        for (int nextIndex = j + 1; nextIndex < _input[i].Length; nextIndex++)
        {
            if (!char.IsDigit(_input[i][nextIndex]) || visitedGrid[i, nextIndex]) break;
            visitedGrid[i, nextIndex] = true;
            builder.Append(_input[i][nextIndex]);
        }

        return int.Parse(builder.ToString());
    }
}