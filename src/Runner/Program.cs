using AoCHelper;
using System.Reflection;
using Common;

Dictionary<uint, Assembly> yearToAssembly = new Dictionary<uint, Assembly>
{
    { 2020, Assembly.GetAssembly(typeof(AoC_2020.Base2020Day))! },
    { 2021, Assembly.GetAssembly(typeof(AoC_2021.Base2021Day))! },
    { 2022, Assembly.GetAssembly(typeof(AoC_2022.Day02))! },
    { 2023, Assembly.GetAssembly(typeof(AoC_2023.Day01))! }
};

List<Assembly> GetAssemblies(uint? year = null)
{
    if (!year.HasValue) return yearToAssembly.Values.ToList();

    return yearToAssembly.TryGetValue(year.Value, out var assemblies)
        ? new List<Assembly> { assemblies }
        : new List<Assembly>();
}

uint year;
if (args.Length == 0)
{
    await Solver.SolveLast(opt =>
    {
        opt.ClearConsole = false;
        opt.ProblemAssemblies = [.. GetAssemblies(), .. opt.ProblemAssemblies];
    });
}
else if (args.Length == 1 && args[0].Contains("all", StringComparison.CurrentCultureIgnoreCase))
{
    await Solver.SolveAll(opt =>
    {
        opt.ShowConstructorElapsedTime = true;
        opt.ShowTotalElapsedTimePerDay = true;
        opt.ProblemAssemblies = [.. GetAssemblies(), .. opt.ProblemAssemblies];
    });
}
else if (args.Length == 1 && uint.TryParse(args[0], out year))
{
    await Solver.SolveLast(opt =>
    {
        opt.ShowConstructorElapsedTime = true;
        opt.ShowTotalElapsedTimePerDay = true;
        opt.ProblemAssemblies = [.. GetAssemblies(year), .. opt.ProblemAssemblies];
    });
}
else if (args.Length == 2 && uint.TryParse(args[0], out year)
                          && args[1].Contains("all", StringComparison.CurrentCultureIgnoreCase))
{
    await Solver.SolveAll(opt =>
    {
        opt.ShowConstructorElapsedTime = true;
        opt.ShowTotalElapsedTimePerDay = true;
        opt.ProblemAssemblies = [.. GetAssemblies(year), .. opt.ProblemAssemblies];
    });
}
else
{
    if (!uint.TryParse(args[0], out year)) year = 0;

    var indexes = args.Skip(1).Select(arg => uint.TryParse(arg, out var index) ? index : uint.MaxValue);

    await Solver.Solve(
        indexes.Where(i => i < uint.MaxValue),
        opt => opt.ProblemAssemblies = [.. GetAssemblies(year), .. opt.ProblemAssemblies]);
}