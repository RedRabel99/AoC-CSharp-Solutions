using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2024;

public class Day04 : BaseLibraryDay
{
    private readonly string[] _input;

    public Day04()
    {
        _input = File.ReadAllLines(InputFilePath);
    }

    protected override int Year => 2024;

    public override ValueTask<string> Solve_1()
    {
        // though it would be cool to do it with recursion but its really slow so i will propably change that in future
        var sum  = 0;
        for(var i = 0; i < _input.Length; i++)
        {
            for(var j = 0;  j < _input[i].Length; j++)
            {
                sum += GetNumberOfAppearances(i, j);
            }
        }

        return new(sum.ToString());
    }

    private int GetNumberOfAppearances(int i, int j)
    {
        var word = new char[] { 'X', 'M', 'A', 'S' };
        var sum = 0;
        if (_input[i][j] != word[0]) return sum;
        
        foreach(var direction in GetAllDirections())
        {
            sum += DoesWordAppear(word, i + direction.Item1, j + direction.Item2, direction.Item1, direction.Item2, 1) ? 1 : 0;
        }
        return sum;

    }

    private List<(int, int)> GetAllDirections()
    {
        int[] values = { 0, 1, -1 };
        var combinations = new List<(int, int)>();
        foreach (var first in values)
        {
            foreach (var second in values)
            {
                if (first == 0 && second == 0) continue;
                combinations.Add((first, second));
            }
        }
        return combinations;
    }

    private bool DoesWordAppear(char[] word,int i, int j, int verticalDirection, int horizontalDirection, int currentWordLength)
    {
        try
        {
            var letter = _input[i][j];
            if (letter != word[currentWordLength]) return false;
            currentWordLength++;
            if(letter == word[word.Length - 1]) return true;
            return DoesWordAppear(word, i + verticalDirection, j +  horizontalDirection, verticalDirection, horizontalDirection, currentWordLength);

        }
        catch (IndexOutOfRangeException)
        {
            return false;
        }
    }

    public override ValueTask<string> Solve_2()
    {
        var sum = 0;
        for (var i = 0; i < _input.Length; i++)
        {
            for (var j = 0; j < _input[i].Length; j++)
            {
                sum += IsXMAS(i, j) ? 1 : 0;
            }
        }

        return new(sum.ToString());
    }

    private bool IsXMAS(int i, int j)
    {
        if(_input[i][j] != 'A') return false;
        try
        {
            bool diagonal1 = (_input[i - 1][j - 1] == 'M' && _input[i + 1][j + 1] == 'S') || 
                             (_input[i - 1][j - 1] == 'S' && _input[i + 1][j + 1] == 'M');

            bool diagonal2 = (_input[i - 1][j + 1] == 'M' && _input[i + 1][j - 1] == 'S') ||
                             (_input[i - 1][j + 1] == 'S' && _input[i + 1][j - 1] == 'M');

            return diagonal1 && diagonal2;
        }
        catch (IndexOutOfRangeException)
        {
            return false;
        }
    }
}
