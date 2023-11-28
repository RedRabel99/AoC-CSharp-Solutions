using AoCHelper;
using System.Reflection;

List<Assembly> assemblies = [
    Assembly.GetAssembly(typeof(AoC_2020.Base2020Day))!,
    Assembly.GetAssembly(typeof(AoC_2021.Base2021Day))!,
    Assembly.GetAssembly(typeof(AoC_2022.Day02))!
];

if (args.Length == 0)
{
    await Solver.SolveLast(opt =>
    {
        opt.ClearConsole = false;
        opt.ProblemAssemblies = [.. assemblies, .. opt.ProblemAssemblies];
    });
}
else if (args.Length == 1 && args[0].Contains("all", StringComparison.CurrentCultureIgnoreCase))
{
    await Solver.SolveAll(opt =>
    {
        opt.ShowConstructorElapsedTime = true;
        opt.ShowTotalElapsedTimePerDay = true;
        opt.ProblemAssemblies = [.. assemblies, .. opt.ProblemAssemblies];
    });
}
else
{
    var indexes = args.Select(arg => uint.TryParse(arg, out var index) ? index : uint.MaxValue);

    await Solver.Solve(
        indexes.Where(i => i < uint.MaxValue),
        opt => opt.ProblemAssemblies = [.. assemblies, .. opt.ProblemAssemblies]);
}
