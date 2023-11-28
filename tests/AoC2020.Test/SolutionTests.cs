using AoCHelper;

namespace AoC_2020.Test;

#pragma warning disable IL2067 // Target parameter argument does not satisfy 'DynamicallyAccessedMembersAttribute' in call to target method. The parameter of method does not have matching annotations.
public static class SolutionTests
{
    [TestCase(typeof(Day_01), "<Solution to Day 1 2020, parts 1 and 2>", "<Solution to Day 1 2020, parts 1 and 2>")]
    public static async Task Test(Type type, string sol1, string sol2)
    {
        if (Activator.CreateInstance(type) is BaseProblem instance)
        {
            await Assert.ThatAsync(async () => await instance.Solve_1(), Is.EqualTo(sol1));
            await Assert.ThatAsync(async () => await instance.Solve_2(), Is.EqualTo(sol2));
        }
        else
        {
            Assert.Fail($"{type} is not a BaseDay");
        }
    }
}
#pragma warning restore IL2067 // Target parameter argument does not satisfy 'DynamicallyAccessedMembersAttribute' in call to target method. The parameter of method does not have matching annotations.
