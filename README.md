# AdventOfCode.MultiYearTemplate

![CI](https://github.com/eduherminio/AdventOfCode.MultiYearTemplate/workflows/CI/badge.svg)

Advent of Code template based on [AoCHelper](https://github.com/eduherminio/AoCHelper) project, that showcases how to keep problems from multiple years in the same repositor by yusing one project per year.

⚠️ If you're not familiar with [AoCHelper](https://github.com/eduherminio/AoCHelper) and the basic template, please have a look at [eduherminio/AdventOfCode.Template](https://github.com/eduherminio/AdventOfCode.Template) first, a simpler template (based on one repository per year approach) where the Solver and the Day classesare in the same assembly.

[AoCHelper README file](https://github.com/eduherminio/AoCHelper#advanced-usage) also includes valuable information about how to use and extend the library.

## Usage

### Base library day

Base library day example, with the minimum changes required to make this approach work.
In the actual template code this class it's split into a common, base day for all the projects and specific ones for each project (which override the year).

```csharp
using AoCHelper;
using System.Reflection;

namespace AoCh2033;

/// <summary>
/// This implementation relies on having different Inputs_{Year} directories per assembly/library
/// </summary>
public abstract class BaseDay2033 : BaseDay
{
    protected int Year => 2033;

    /// <summary>
    /// Two purposes:
    /// 1. Required to make sure `dotnet run` uses output directory files, since problems aren't located in the assembly where <see cref="Solver"/> is used.
    /// 2. Since input files for different years have the same name, they would override each other in the output directory Inputs folder if we're not careful.
    /// </summary>
    protected override string InputFileDirPath =>
        Path.Combine(
            Path.GetDirectoryName(Assembly.GetEntryAssembly()!.Location)!,      // Takes care of concern 1
            $"{base.InputFileDirPath}_{Year}");                                 // Takes care of concern 2
}
```

Alternative way of solving the second concern, if you don't want to have different Inputs directory names per assembly/library:

```csharp
using AoCHelper;
using System.Reflection;

namespace AoCh2033;

/// <summary>
/// This implementation relies on having different file names per assembly/library (i.e. YYYY_dd.txt)
/// </summary>
public abstract class BaseDay2033 : BaseDay
{
    protected int Year => 2033;

    /// <summary>
    /// Required to make sure `dotnet run` uses output directory files, since problems aren't located in the assembly
    /// where <see cref="Solver"/> is used.
    /// </summary>
    protected override string InputFileDirPath =>
        Path.Combine(
            Path.GetDirectoryName(Assembly.GetEntryAssembly()!.Location)!,
            base.InputFileDirPath);

    /// <summary>
    /// Expects '{Year}_' before the usual file name, i.e. 2033_01.txt.
    /// Based in <see cref="AoCHelper.BaseProblem.InputFilePath"/> original implementation
    /// </summary>
    public override string InputFilePath
    {
        get
        {
            var index = CalculateIndex().ToString("D2");

            return Path.Combine(InputFileDirPath, $"{Year}_{index}.{InputFileExtension.TrimStart('.')}");
        }
    }
}
```

### Solver usage

Basic 'runner' example:

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

You probably want to customize this so that it accepts a command line argument to specify the year to run.
Or, if you're lazy, you can just comment the assembly lines of previous years.
