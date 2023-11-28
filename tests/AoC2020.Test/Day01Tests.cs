using NUnit.Framework.Legacy;

namespace AoC_2020.Test;

class Day_01Test : Day_01
{
    public override string InputFilePath => "TestInputs/01-example.txt";
}

public class Day01Tests
{
    [Test]
    public async Task SampleInput()
    {
        const string solution = "<2020 Day 1 TEST file input content>";

        var day = new Day_01Test();

        ClassicAssert.AreEqual(solution, await day.Solve_1());
        ClassicAssert.AreEqual(solution, await day.Solve_2());
    }
}
