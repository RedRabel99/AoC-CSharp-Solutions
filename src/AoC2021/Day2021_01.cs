using Common;

namespace AoC_2021;

public class Day2021_01 : Base2021Day
{
    public Day2021_01()
    {
        _ = File.ReadAllText(InputFilePath);
    }

    public override ValueTask<string> Solve_1() => new($"Solution to {ClassPrefix} {CalculateIndex()} {Year}, part 1");

    public override ValueTask<string> Solve_2() => new($"Solution to {ClassPrefix} {CalculateIndex()} {Year}, part 2");
}