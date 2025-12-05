using Common;

namespace AoC2025;

public class Day04 : BaseLibraryDay
{
    protected override int Year => 2025;
    private char[][] _input;

    public Day04()
    {
        _input = File.ReadAllLines(InputFilePath).Select(x => x.ToCharArray()).ToArray();
    }

    public override ValueTask<string> Solve_1()
    {
        int sum = 0;
        int heigth = _input.Length;
        int width = _input[0].Length;
        for (int i = 0; i < heigth; i++)
        {
            for(int j = 0; j < width; j++)
            {
                sum += CountAdjacent(i, j, '@') ? 1 : 0;
            }
        }

        return new(sum.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        int sum = 0;
        int heigth = _input.Length;
        int width = _input[0].Length;
        int currentSum = 1;
        while (currentSum > 0) 
        {
            currentSum = 0;
            for (int i = 0; i < heigth; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if(CountAdjacent(i, j, '@'))
                    {
                        currentSum++;
                        _input[i][j] = '.';
                    }
                }
            }
            sum += currentSum;

        }

        return new(sum.ToString());
    }

    private bool CountAdjacent(int i, int j, char c)
    {
        if (_input[i][j] != c) return false;
        int heigth = _input.Length;
        int width = _input[0].Length;
        int sum = 0;
        if (i > 0 && j < heigth - 1 && _input[i - 1][j + 1] == c) sum++;
        if (i > 0 && _input[i - 1][j] == c) sum++;
        if (i > 0 && j > 0 && _input[i - 1][j - 1] == c) sum++;
        if (j > 0 && _input[i][j - 1] == c) sum++;
        if (j < heigth - 1 && _input[i][j + 1] == c) sum++;
        if (i < width - 1 && j > 0 && _input[i + 1][j - 1] == c) sum++;
        if (i < width - 1 && _input[i + 1][j] == c) sum++;
        if (i < width - 1 && j < heigth - 1 && _input[i + 1][j + 1] == c) sum++;

        return sum < 4;
    }
}
