using AoCHelper;
using System.Reflection;

namespace AoC_2021;

/// <summary>
/// This implementation relies on having different file names per assembly/library (i.e. YYYY_dd.txt),
/// allowing us to use the same 'Inputs' directory name
/// </summary>
public abstract class Base2021Day : BaseDay
{
    protected const int Year = 2021;

    /// <summary>
    /// Additional customization
    /// </summary>
    protected override string ClassPrefix => base.ClassPrefix + Year;

    /// <summary>
    /// Required to make sure `dotnet run` uses output directory files, since problems aren't located in the assembly
    /// where <see cref="Solver"/> is used.
    /// </summary>
    protected override string InputFileDirPath =>
        Path.Combine(
            Path.GetDirectoryName(Assembly.GetEntryAssembly()!.Location)!,
            base.InputFileDirPath);

    /// <summary>
    /// Expects '{Year}_' before the usual file name, i.e. 2021_01.txt
    /// Based in <see cref="AoCHelper.BaseProblem.InputFilePath"/>.    /// </summary>
    public override string InputFilePath
    {
        get
        {
            var index = CalculateIndex().ToString("D2");

            return Path.Combine(InputFileDirPath, $"{Year}_{index}.{InputFileExtension.TrimStart('.')}");
        }
    }
}