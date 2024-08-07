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
    protected override int Year => 2023;

    public Day02()
    {
        _input = File.ReadAllLines(InputFilePath);
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

    public override ValueTask<string> Solve_1()
    {
        var games = ParseInput();
        var sum = 0;
        for (var i = 0; i < games.Count; i++)
        {
            if (CanGameBePlayed(games[i])) sum += i + 1;
        }

        return new(sum.ToString());
    }

    public override ValueTask<string> Solve_2() => new($"Solution to {ClassPrefix} {CalculateIndex()} {Year} part 2");
}