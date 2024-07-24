using Common;

namespace AoC_2022;

public class Day02 : BaseLibraryDay
{
    /// <summary>
    /// Since we're not using a base day, we need to override the year in every day class
    /// </summary>
    protected override int Year => 2022;

    public Day02()
    {
        _ = File.ReadAllText(InputFilePath);
    }

    public override ValueTask<string> Solve_1() => new($"Solution to {ClassPrefix} {CalculateIndex()} {Year}, part 1");

    public override ValueTask<string> Solve_2() => new($"Solution to {ClassPrefix} {CalculateIndex()} {Year} part 2");
}