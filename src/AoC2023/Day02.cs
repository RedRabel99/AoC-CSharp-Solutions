using Common;

namespace AoC_2023;

internal struct GameSet
{
    public int Red;
    public int Green;
    public int Blue;
}

public class Day02 : BaseLibraryDay
{
    private readonly string[] _input;
    private readonly List<List<GameSet>> _games;
    protected override int Year => 2023;

    public Day02()
    {
        _input = File.ReadAllLines(InputFilePath);
        _games = ParseInput();
    }

    private static GameSet ParseGameSet(string setLine)
    {
        var gameSet = new GameSet();
        foreach (var cubes in setLine.Split(','))
        {
            var values = cubes.Trim().Split(" ");
            switch (values[1])
            {
                case "blue":
                    gameSet.Blue = int.Parse(values[0]);
                    break;
                case "red":
                    gameSet.Red = int.Parse(values[0]);
                    break;
                case "green":
                    gameSet.Green = int.Parse(values[0]);
                    break;
            }
        }

        return gameSet;
    }

    private List<List<GameSet>> ParseInput()
    {
        var result = new List<List<GameSet>>();
        foreach (var line in _input)
        {
            var setList = new List<GameSet>();
            var allSets = line.Split(":")[1];
            foreach (var set in allSets.Split(";"))
            {
                setList.Add(ParseGameSet(set));
            }

            result.Add(setList);
        }

        return result;
    }

    private static bool CanGameBePlayed(List<GameSet> gameSets)
    {
        foreach (var set in gameSets)
        {
            if (set.Red > 12) return false;
            if (set.Green > 13) return false;
            if (set.Blue > 14) return false;
        }

        return true;
    }

    private static int GetPowerOfSet(List<GameSet> gameSets)
    {
        var maxRed = 0;
        var maxGreen = 0;
        var maxBlue = 0;

        foreach (var set in gameSets)
        {
            if (set.Red > maxRed) maxRed = set.Red;
            if (set.Green > maxGreen) maxGreen = set.Green;
            if (set.Blue > maxBlue) maxBlue = set.Blue;
        }

        return maxRed * maxGreen * maxBlue;
    }

    public override ValueTask<string> Solve_1()
    {
        var sum = 0;
        for (var i = 0; i < _games.Count; i++)
        {
            if (CanGameBePlayed(_games[i])) sum += i + 1;
        }

        return new(sum.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        var sum = 0;
        foreach (var gameSets in _games)
        {
            sum += GetPowerOfSet(gameSets);
        }

        return new(sum.ToString());
    }
}