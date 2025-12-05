using Common;

namespace AoC2025;

public class Day05 : BaseLibraryDay
{
    protected override int Year => 2025;
    private string[] _input;
    private long[][] ranges;
    private long[] ids;

    public Day05()
    {
        _input = File.ReadAllLines(InputFilePath);
        ParseInput();
    }

    private void ParseInput()
    {
        var gap = _input.IndexOf("");
        ranges = _input.Take(gap).Select(x => x.Split('-').Select(long.Parse).ToArray()).ToArray();
        ids = _input.Skip(gap + 1).Select(long.Parse).ToArray();
    }

    public override ValueTask<string> Solve_1()
    {
        var result = ids.Count(i => ranges.Any(r => r[0] <= i && r[1] >= i));

        return new(result.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        var result = 0;

        return new(result.ToString());
    }

}