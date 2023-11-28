using Common;

namespace AoC_2020;

public class Day_01 : Base2020Day
{
    private readonly string _input;
    public Day_01()
    {
        _input = File.ReadAllText(InputFilePath);
    }

    public override ValueTask<string> Solve_1() => new(_input);

    public override ValueTask<string> Solve_2() => new(_input);
}