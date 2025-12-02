using Common;

namespace AoC2025;

public class Day01 : BaseLibraryDay
{
    private readonly string[] _input;
    protected override int Year => 2025;

    public Day01()
    {
        _input = File.ReadAllLines(InputFilePath);
    }

    public override ValueTask<string> Solve_1() //1074
    {
        int currentNumber = 50;
        int sum = 0;

        foreach (var instruction in _input)
        {
            char direction = instruction[0];
            int value = int.Parse(instruction.Substring(1));
            if (direction == 'R')
            {
                currentNumber = (currentNumber + value) % 100;
            }
            else
            {
                int substractResult = currentNumber - value;
                while (substractResult < 0) substractResult += 100;
                currentNumber = substractResult;
            }
            if(currentNumber == 0) sum++;
        }

        return new(sum.ToString());
    }

    public override ValueTask<string> Solve_2() //6245
    {
        int currentNumber = 50;
        int sum = 0;

        foreach (var instruction in _input)
        {
            char direction = instruction[0];
            int value = int.Parse(instruction.Substring(1));
            if (direction == 'R')
            {
                sum += (currentNumber + value % 100 > 99 ? 1 : 0 ) + value / 100;
                currentNumber = (currentNumber + value) % 100;
            }
            else
            {
                if (currentNumber == 0) sum--; //substracting one zero cause it will be counted anyway below
                sum += (currentNumber - value % 100 <= 0 ? 1 : 0 ) + value / 100;
                int substractResult = currentNumber - (value % 100);
                currentNumber = substractResult >= 0 ? substractResult : substractResult + 100;
            }
            
        }

        return new(sum.ToString());
    }
}
