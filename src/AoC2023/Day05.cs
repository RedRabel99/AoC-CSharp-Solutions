using Common;
using System.Collections;

namespace AoC_2023;


public class Day05 : BaseLibraryDay
{
    private readonly string[] _input;
    private List<long> _seeds;
    private List<List<MapRange>> _maps = new List<List<MapRange>>();

    protected override int Year => 2023;

    public Day05()
    {
        _input = File.ReadAllLines(InputFilePath);
        ParseInput();
    }

    private void ParseInput()
    {
        _seeds = _input[0]
            .Split(":")[1]
            .Split(" ", StringSplitOptions.RemoveEmptyEntries)
            .Select(long.Parse)
            .ToList();

        var mapCounter = 0;

        for (var i = 2; i < _input.Length; i++)
        {
            if (_input[i] == "") continue;

            if (_input[i].EndsWith(":"))
            {
                _maps.Add(new List<MapRange>());
                mapCounter++;
            }
            else
            {
                var ranges = _input[i]
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                    .Select(long.Parse)
                    .ToArray();

                _maps[mapCounter - 1].Add(new MapRange(ranges[0], ranges[1], ranges[2]));
            }
        }
    }

    public long MapElement(List<MapRange> mapRanges, long value)
    {
        foreach (var range in mapRanges)
        {
            if (value >= range.SourceStart && value < range.SourceStart + range.RangeLength)
            {
                return range.DestinationStart + Math.Abs(range.SourceStart - value);
            }

        }

        return value;
    }

    //brute-force
    public override ValueTask<string> Solve_1()
    {
        var result = new long[_seeds.Count];
        for (int i = 0; i < _seeds.Count; i++)
        {
            var currentSeedValue = _seeds[i];
            foreach (var map in _maps)
            {
                currentSeedValue = MapElement(map, currentSeedValue);
            }
            result[i] = currentSeedValue;
        }

        return new($"{result.Min()}");
    }

    public override ValueTask<string> Solve_2()
    {
        var seedRanges = new Queue<SeedRange>();
        for(int i = 0; i < _seeds.Count; i += 2)
        {
            seedRanges.Enqueue(new SeedRange(_seeds[i], _seeds[i + 1]));
        }

        foreach(var map in _maps)
        {
            var newSeedRange = new Queue<SeedRange>();
            while (seedRanges.Count > 0)
            {
                var seedRange = seedRanges.Dequeue();
                var mapMatched = false;
                foreach(var mapRange in map)
                {

                    var overlapStart = Math.Max(seedRange.First, mapRange.SourceStart);
                    var overlapEnd = Math.Min(seedRange.First + seedRange.Length, mapRange.SourceStart + mapRange.RangeLength);

                    if(overlapStart < overlapEnd)
                    {
                        newSeedRange.Enqueue(
                            new SeedRange(
                                overlapStart + mapRange.DestinationStart - mapRange.SourceStart,
                                overlapEnd - overlapStart
                            ));
                        if (seedRange.First < overlapStart)
                        {
                            seedRanges.Enqueue(
                                new SeedRange(
                                    seedRange.First,
                                    overlapStart - seedRange.First
                                ));
                        }
                        if (seedRange.First + seedRange.Length > overlapEnd)
                        {
                            seedRanges.Enqueue(
                                new SeedRange(
                                    overlapEnd,
                                    seedRange.First + seedRange.Length - overlapEnd
                                ));
                        }
                        mapMatched = true;
                        break;
                    }

                }
                if(mapMatched == false)
                {
                    newSeedRange.Enqueue(seedRange);
                }
            }
            seedRanges = newSeedRange;
        }
        var result = seedRanges.Min( x => x.First);
        return new($"{result}");

    }
}

public record SeedRange(long First, long Length);

public record MapRange(long DestinationStart, long SourceStart, long RangeLength);