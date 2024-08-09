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

    private bool CheckForSymbols(int i, int j, int height, int width)
    {
        if (i > 0 && j > 0 && (!char.IsDigit(_input[i - 1][j - 1]) && _input[i - 1][j - 1] != '.')) return true; //left up
        if (i > 0 && (!char.IsDigit(_input[i - 1][j]) && _input[i - 1][j] != '.')) return true; //up 
        if (i > 0 && j < (width - 1) && (!char.IsDigit(_input[i - 1][j + 1]) && _input[i - 1][j + 1] != '.')) return true; // right up
        if (j < (width - 1) && (!char.IsDigit(_input[i][j + 1]) && _input[i][j + 1] != '.')) return true; //right
        if (i < (height - 1) && j < (width - 1) && (!char.IsDigit(_input[i + 1][j + 1]) && _input[i + 1][j + 1] != '.')) return true; //right bottom
        if (i < (height - 1) && (!char.IsDigit(_input[i + 1][j]) && _input[i + 1][j] != '.')) return true; // bottom
        if (i < (height - 1) && j > 0 && (!char.IsDigit(_input[i + 1][j - 1]) && _input[i + 1][j - 1] != '.')) return true; // left bottom
        if (j > 0 && (!char.IsDigit(_input[i][j - 1]) && _input[i][j - 1] != '.')) return true; //left
        return false;
    }
    private int GetPartValue(int i, int j)
    {
        var height = _input.Length;
        var width = _input[0].Length;
        var hasAdjacentSymbol = false;
        var value = "";
        var numberIndexes = new List<int>();
        while (j < width && char.IsDigit(_input[i][j]))
        {
            numberIndexes.Add(j);
            value += _input[i][j];
            j++;
        }

        return numberIndexes.Any(x => CheckForSymbols(i, x, height, width)) ? int.Parse(value) : 0;
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

    public override ValueTask<string> Solve_2() => new($"Solution to {ClassPrefix} {CalculateIndex()} {Year} part 2");
}