using AoCHelper;
using System.Reflection;

namespace Common;

public abstract class BaseLibraryDay : BaseDay
{
    /// <summary>
    /// Required to make sure `dotnet run` uses output directory files
    /// </summary>
    public override string InputFilePath =>
        Path.Combine(
            Path.GetDirectoryName(Assembly.GetEntryAssembly()!.Location)!,
            base.InputFilePath);

    /// <summary>
    /// Can't have all years input mixed up if they're expected to have the same format.
    /// </summary>
    protected override string InputFileDirPath => $"{base.InputFileDirPath}_{Year}";

    protected abstract int Year { get; }
}