using Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace AoC2025;

public class Day09 : BaseLibraryDay
{
    protected override int Year => 2025;
    public string[] _input;
    private List<(long x, long y)> points;

    public Day09()
    {
        _input = File.ReadAllLines(InputFilePath);
        ParseInput();
    }

    private void ParseInput()
    {
        points = _input.Select(x => {
            var points = x.Split(',');
            return (long.Parse(points[0]),long.Parse(points[1]));
        }).ToList();
    }

    public override ValueTask<string> Solve_1()
    {
        //int max = 0;
        //var points = new List<(long x, long y)>   ();
        //for(int i = 0; i < _input.Length; i++)
        //{
        //    for(int j = 0;  j < _input[i].Length; j++)
        //    {
        //        if( _input[i][j] == '#') points.Add((x: j, y: i) );
        //    }
        //}

        return new(points.SelectMany((value, index) =>
            points.Skip(index + 1), (first, second) => new { first, second }).Where(x => IsInBounds(x.first, x.second))
                .Select(x => (Math.Abs(x.first.x - x.second.x) + 1 ) * (Math.Abs(x.first.y - x.second.y) + 1)).Max().ToString()
            );
    }

    public override ValueTask<string> Solve_2()
    {
        return new(points.SelectMany((value, index) =>
           points.Skip(index + 1), (first, second) => new { first, second })
               .Select(x => (Math.Abs(x.first.x - x.second.x) + 1) * (Math.Abs(x.first.y - x.second.y) + 1)).Max().ToString()
           );
    }
    
    
    private bool IsInBounds((long x, long y) first, (long x, long y) second) 
    {
        var firstToCheck = (x: first.x, y: second.y);
        var secondToCheck = (x: second.x, y: first.y);
            

        return (
            points.Any(currentPoint =>(
                !(firstToCheck.x == currentPoint.x && firstToCheck.y < currentPoint.y) || //downward
                !(firstToCheck.x == currentPoint.x && firstToCheck.y > currentPoint.y) ||   //upward
                !(firstToCheck.y == currentPoint.y && firstToCheck.x < currentPoint.x) ||
                !(firstToCheck.x == currentPoint.x && firstToCheck.y < currentPoint.y)) &&
                !(secondToCheck.x == currentPoint.x && secondToCheck.y < currentPoint.y) || //downward
                !(secondToCheck.x == currentPoint.x && secondToCheck.y > currentPoint.y) ||   //upward
                !(secondToCheck.y == currentPoint.y && secondToCheck.x < currentPoint.x) ||
                !(secondToCheck.x == currentPoint.x && secondToCheck.y < currentPoint.y))
            );
    }

}
