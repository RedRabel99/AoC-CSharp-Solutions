using Common;

namespace AoC2025;

public class Day03 : BaseLibraryDay
{
    protected override int Year => 2025;
    private IEnumerable<int[]> _input;

    public Day03()
    {
        _input = File.ReadAllLines(InputFilePath).Select(x =>
            x.Select(c =>
                (int)char.GetNumericValue(c)
            ).ToArray());
    }

    public override ValueTask<string> Solve_1()
    {
        int sum = 0;
        foreach (var bank in _input)
        {
            int currentMax = 0;
            for (int i = 0; i < bank.Length - 1; i++)
            {
                var currentValue = bank[i] * 10 + bank.Skip(i + 1).Max();
                if (currentValue > currentMax) currentMax = currentValue;
            }
            sum += currentMax;
        }
        return new(sum.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        long sum = 0;
        foreach (var bank in _input)
        {
            long current = 0;
            int start = 0;
            for(int i = 12; i > 0; i--)
            {
                var maxIndex = GetMaxItemIndex(bank, start, bank.Length - i);
                current += bank[maxIndex] * (long)Math.Pow(10, i-1);
                start = maxIndex + 1;
            }
            sum += current;
        }

        return new(sum.ToString());
    }

    private int GetMaxItemIndex(int[] bank, int start, int end)
    {
        var currentIndex = 0;
        var currentMax = 0;

        for (var i = start; i <= end; i++)
        {
            if (bank[i] > currentMax)
            {
                currentMax = bank[i];
                currentIndex = i;
            }
        }
        return currentIndex;
    }
}
