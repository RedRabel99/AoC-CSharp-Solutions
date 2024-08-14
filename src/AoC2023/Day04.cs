﻿using Common;

namespace AoC_2023;

public class Day04 : BaseLibraryDay
{
    private readonly string[] _input;
    
    protected override int Year => 2023;

    public Day04()
    {
        _input = File.ReadAllLines(InputFilePath);
    }

    private List<Tuple<List<string>, List<string>>> ParseInput()
    {
        var result = new List<Tuple<List<string>, List<string>>>();
        foreach (var line in _input)
        {
            var numbers = line.Split(":")[1].Split("|");
            var card = new Tuple<List<string>, List<string>>(numbers[0].Trim().Split().ToList(),
                numbers[1].Trim().Split().ToList());
            card.Item1.RemoveAll(item => item == "");
            card.Item2.RemoveAll(item => item == "");
            result.Add(card);
        }

        return result;
    }

    public override ValueTask<string> Solve_1()
    {
        var cards = ParseInput();
        var sum = 0;
        foreach (var card in cards)
        {
            var count = card.Item1.Count(a => card.Item2.Contains(a));
            
            sum += count != 0 ? (int)Math.Pow(2, count - 1) : 0;
        }
        return new($"{sum}");
    }

    public override ValueTask<string> Solve_2()
    {
        return new($"0");
    }
}