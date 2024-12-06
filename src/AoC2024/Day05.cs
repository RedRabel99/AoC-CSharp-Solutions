using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AoC2024;

public class Day05 : BaseLibraryDay
{
    public readonly string[]  _input;
    public Dictionary<string, List<string>> rules;
    public List<List<string>> updates;
    public Day05()
    {
        _input = File.ReadAllLines(InputFilePath);
        ParseInput();
    }

    private void ParseInput()
    {
        int i = 0;
        rules = new Dictionary<string, List<string>>();
        for(; i < _input.Length; i++)
        {
            if (_input[i] == "") break;
            var rule = _input[i].Split('|');
            if(rules.ContainsKey(rule[0])) rules[rule[0]].Add(rule[1]);
            else rules[rule[0]] = new List<string> { rule[1] };
        }

        updates = new List<List<string>>();
        for(int j = i + 1; j < _input.Length; j++)
        {
            updates.Add(_input[j].Split(',', StringSplitOptions.RemoveEmptyEntries).ToList());
        }
        Console.WriteLine("");
    }

    protected override int Year => 2024;

    public override ValueTask<string> Solve_1()
    {
        var  sum = 0;
        foreach(var update in updates)
        {
            if (IsCorrect(update))
            {
                var index = update.Count / 2;
                var value  = int.Parse(update[index]);
                sum += value;
            }
        }

        return new(sum.ToString());
    }

    private bool IsCorrect(List<string> update)
    {
        for(int i = 0; i < update.Count; i++)
        {
            var currentNumber = update[i];
            try
            {
                for(int j = i + 1;j < update.Count; j++)
                {
                    if (!rules[currentNumber].Contains(update[j])) return false;
                }
                for (int j = i - 1; j >= 0; j--)
                {
                    if (rules[currentNumber].Contains(update[j])) return false;
                }
            }
            catch(KeyNotFoundException)
            {
                continue;
            }
            
        }
        return true;
    }

    public override ValueTask<string> Solve_2()
    {
        var sum = 0;
        foreach (var update in updates)
        {
            if (IsCorrect(update))
            {
                continue;
            }
            update.Sort(CompareByRules);
            var value = int.Parse(update[update.Count/2]);
            sum += value;
        }

        return new(sum.ToString());
    }

    private int CompareByRules(string x, string y)
    {
        if (rules.ContainsKey(x))
        {
            if (rules[x].Contains(y)) return -1;
        }
        if(rules.ContainsKey(y))
        {
            if(rules[y].Contains(x)) return 1;
        }
        return 0;
    }
}
