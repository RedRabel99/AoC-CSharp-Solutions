using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2024;

public class Day02 : BaseLibraryDay
{
    private readonly string[] _input;

    public Day02()
    {
        _input = File.ReadAllLines(InputFilePath);
    }

    protected override int Year => 2024;

    public bool IsSafe(int[] report)
    {
        var difference = report[1] - report[0];
        if (difference == 0 || Math.Abs(difference) > 3) return false;
        var IsAscending = difference > 0;
        for(var i = 2; i < report.Length; i++)
        {
            difference = report[i] - report[i - 1];
            if (difference == 0 || Math.Abs(difference) > 3) return false;
            if(IsAscending != (difference > 0 )) return false;
        }
        return true;
    }

    public override ValueTask<string> Solve_1()
    {

        var reports = _input.Select(
           line => line.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray()).ToArray();

        var counter = 0;
        foreach (var report in reports)
        {
            if (IsSafe(report.ToArray()) == true)
            {
                counter++;
            }
        }
        return new($"{counter}");
    }

    public override ValueTask<string> Solve_2()
    {
        var reports = _input.Select(
           line => line.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList()).ToList();
        var counter = 0;
        foreach (var report in reports)
        {
            if(IsSafe(report.ToArray()) == true)
            {
                counter++;
                continue;
            }
            
            for(var i = 0; i < report.Count; i++)
            {
                var newArray = report.Where((_, j) => j != i).ToArray();
                if (IsSafe(newArray))
                {
                    counter++;
                    break;
                }
                
            }
        }

        return new($"{counter}");
    }
}
