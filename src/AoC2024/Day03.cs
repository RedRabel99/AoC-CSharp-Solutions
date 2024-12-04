using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AoC2024;

public class Day03 : BaseLibraryDay
{
    public readonly string _input;

    public Day03()
    {
        _input = File.ReadAllText(InputFilePath);

    }

    protected override int Year => 2024;

    public override ValueTask<string> Solve_1()
    {
        return new(
            Regex.Matches(_input, @"mul\((\d+),(\d+)\)")
                .Select(match =>
                    int.Parse(match.Groups[1].Value) * int.Parse(match.Groups[2].Value)
                )
                .Sum()
                .ToString()
        );
    }

    public override ValueTask<string> Solve_2()
    {
        return new(
            Regex.Matches(_input, @"do\(\)|don't\(\)|mul\((\d+),(\d+)\)")
                .Aggregate(
                    (Enabled: true, Sum: 0),
                    (state, match) =>
                    {
                        if (match.Value == "do()")
                            return (Enabled: true, state.Sum);

                        if (match.Value == "don't()")
                            return (Enabled: false, state.Sum);

                        if (state.Enabled)
                            return (
                                state.Enabled,
                                state.Sum + int.Parse(match.Groups[1].Value) * int.Parse(match.Groups[2].Value)
                            );

                        return (state.Enabled, state.Sum);
                    }
                )
                .Sum
                .ToString()
        );
    }
}
