using Common;

namespace AoC_2023;

public class Day01 : BaseLibraryDay
{
    private readonly string[] _input;

    private readonly Dictionary<string, int> _numbers = new()
    {
        { "one", 1 },
        { "two", 2 },
        { "three", 3 },
        { "four", 4 },
        { "five", 5 },
        { "six", 6 },
        { "seven", 7 },
        { "eight", 8 },
        { "nine", 9 }
    };

    protected override int Year => 2023;

    public Day01()
    {
        _input = File.ReadAllLines(InputFilePath);
    }

    public override ValueTask<string> Solve_1()
    {
        var sum = 0;
        foreach (var line in _input)
        {
            var numbersList = new List<char>();
            foreach (var letter in line)
            {
                if (char.IsDigit(letter)) numbersList.Add(letter);
            }

            sum += int.Parse($"{numbersList[0]}{numbersList[^1]}");
        }

        return new($"{sum}");
    }

    public override ValueTask<string> Solve_2()
    {
        var sum = 0;
        foreach (var line in _input)
        {
            var numbersList = new List<int>();
            for (var i = 0; i < line.Length; i++)
            {
                for (var j = 1; j < line.Length - i + 1; j++)
                {
                    var substring = line.Substring(i, j);
                    if (substring.Length == 1)
                    {
                        if (!char.IsDigit(substring[0])) continue;
                        numbersList.Add(int.Parse(substring));
                        break;
                    }
                    if (!_numbers.TryGetValue(substring, out var value)) continue;
                    numbersList.Add(value);
                    break;
                }
            }
            sum += int.Parse($"{numbersList[0]}{numbersList[^1]}");
        }

        return new($"{sum}");
    }
}