using Common;

namespace AoC_2021;

public abstract class Base2021Day : BaseLibraryDay
{
    protected override int Year => 2021;

    /// <summary>
    /// Additional customization
    /// </summary>
    protected override string ClassPrefix => base.ClassPrefix + Year;

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