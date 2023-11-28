# AdventOfCode.MultiYearTemplate

![CI](https://github.com/eduherminio/AdventOfCode.MultiYearTemplate/workflows/CI/badge.svg)

Advent of Code template based on [AoCHelper](https://github.com/eduherminio/AoCHelper) project that showcases how to keep problems from multiple years in the same repository, using one project per year.

Please have a look at [eduherminio/AdventOfCode.Template](https://github.com/eduherminio/AdventOfCode.Template) for a simpler template corresponding to one repository per year, and to [AoCHelper README file](https://github.com/eduherminio/AoCHelper#advanced-usage) for advance usage of the library.

Problem example:

```csharp
using AoCHelper;
using System.Threading.Tasks;

namespace AoC2033;

public class Day_01 : BaseLibraryDay
{
    protected override int Year => 2033;

    public override ValueTask<string> Solve_1() => new("Solution 1");

    public override ValueTask<string> Solve_2() => new("Solution 2");
}

```

Runner example:

```csharp
using AoCHelper;
using System.Reflection;

await Solver.SolveAll(opt =>
{
    opt.ShowConstructorElapsedTime = true;
    opt.ShowTotalElapsedTimePerDay = true;
    opt.ProblemAssemblies = [
        Assembly.GetAssembly(typeof(AoC_2020.Base2020Day))!,
        Assembly.GetAssembly(typeof(AoC_2021.Base2021Day))!,
        Assembly.GetAssembly(typeof(AoC_2022.Day02))!,
        .. opt.ProblemAssemblies];
});
```
