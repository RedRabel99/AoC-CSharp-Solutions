using AoCHelper;
using System.Reflection;

var assembliesByYear = new Dictionary<uint, Assembly>
{
    [2020] = Assembly.GetAssembly(typeof(AoC_2020.Base2020Day))!,
    [2021] = Assembly.GetAssembly(typeof(AoC_2021.Base2021Day))!,
    [2022] = Assembly.GetAssembly(typeof(AoC_2022.Day02))!
};

List<Assembly> Assemblies(uint year) =>
    assembliesByYear.TryGetValue(year, out var assembly)
        ? [assembly]
        : [];

if (args.Length == 0)
{
    await Solver.SolveLast(opt =>
    {
        opt.ClearConsole = false;
        opt.ProblemAssemblies = [.. assembliesByYear.Values, .. opt.ProblemAssemblies];
    });
}
else if (args.Length == 1)
{
    ICollection<Assembly> yearAssemblies = [];

    if (args[0].Contains("all", StringComparison.CurrentCultureIgnoreCase))
    {
        yearAssemblies = assembliesByYear.Values;
    }
    else if (uint.TryParse(args[0], out var year))
    {
        yearAssemblies = Assemblies(year);
    }

    if (yearAssemblies.Count > 0)
    {
        await Solver.SolveAll(opt =>
        {
            opt.ShowConstructorElapsedTime = true;
            opt.ShowTotalElapsedTimePerDay = true;
            opt.ProblemAssemblies = [.. yearAssemblies, .. opt.ProblemAssemblies];
        });
    }
}
else if (args.Length == 2 &&
    (args[0].Contains("all", StringComparison.CurrentCultureIgnoreCase) || args[1].Contains("all", StringComparison.CurrentCultureIgnoreCase))
    && (uint.TryParse(args[0], out var year) || uint.TryParse(args[1], out year)))
{
    await Solver.SolveAll(opt =>
    {
        opt.ShowConstructorElapsedTime = true;
        opt.ShowTotalElapsedTimePerDay = true;
        opt.ProblemAssemblies = [.. Assemblies(year), .. opt.ProblemAssemblies];
    });
}
else
{
    if (uint.TryParse(args[0], out year))
    {
        var indexes = args[1..]
            .Select(arg => uint.TryParse(arg, out var index)
                ? index
                : uint.MaxValue);

        await Solver.Solve(
            indexes.Where(i => i < uint.MaxValue),
            opt => opt.ProblemAssemblies = [.. Assemblies(year), .. opt.ProblemAssemblies]);
    }
}