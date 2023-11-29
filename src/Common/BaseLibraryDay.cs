using AoCHelper;
using System.Reflection;

namespace Common;

public abstract class BaseLibraryDay : BaseDay
{
    protected abstract int Year { get; }

    /// <summary>
    /// Two purposes:
    /// 1. Required to make sure `dotnet run` uses output directory files, since problems aren't located in the assembly where <see cref="Solver"/> is used.
    /// 2. Since input files for different years have the same name, they would override each other in the output directory Inputs folder if we're not careful.
    /// This implementation relies on having different Inputs_{Year} directories per assembly/library
    /// </summary>
    protected override string InputFileDirPath =>
        Path.Combine(
            Path.GetDirectoryName(Assembly.GetEntryAssembly()!.Location)!,      // Takes care of concern 1
            $"{base.InputFileDirPath}_{Year}");                                 // Takes care of concern 2

}