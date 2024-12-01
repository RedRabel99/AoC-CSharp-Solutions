using Common;
using System.Collections;

namespace AoC2024;

public class Day01 : BaseLibraryDay
{
    private readonly string[] _input;
    private List<int> _firstList = new List<int>();
    private List<int> _secondList = new List<int>();
    protected override int Year => 2024;
    public Day01()
    {
        _input = File.ReadAllLines(InputFilePath);
        ParseInput();
    }

    private void ParseInput()
    {
        foreach (var line in _input)
        {
            var numbers = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            _firstList.Add(int.Parse(numbers[0]));
            _secondList.Add(int.Parse(numbers[1]));
        }
    }
    public override ValueTask<string> Solve_1()
    {
        _firstList.Sort();
        _secondList.Sort();

        var result = _firstList.Zip(_secondList, (first, second) => (First: first, Second: second))
            .Select(x => Math.Abs(x.First - x.Second)).Sum();

        return new($"{result}");
    }

    public override ValueTask<string> Solve_2()
    {
        //var result = Solve_2_WithoutLinq(_firstList, _secondList);
        var result = _firstList.Select(
            x => x * _secondList.Where(y => y == x).Count()
            ).Sum();
        return new($"{result}");
    }

    public int Solve_2_WithoutLinq(List<int> firstList, List<int> secondList)
    {
        var occurances = new Dictionary<int, int>();
        foreach (var number in secondList)
        {
            if (occurances.ContainsKey(number))
            {
                occurances[number]++;
            }
            else
            {
                occurances[number] = 1;
            }
        }


        int sum = 0;
        foreach (var number in firstList)
        {
            if (occurances.ContainsKey(number))
            {
                sum += number * occurances[number];
            }
        }

        return sum;
    }
}
