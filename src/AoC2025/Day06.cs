using Common;

namespace AoC2025
{
    public class Day06 : BaseLibraryDay
    {
        protected override int Year => 2025;
        private string[] _input;

        public Day06()
        {
            _input = File.ReadAllLines(InputFilePath);
        }

        public override ValueTask<string> Solve_1()
        {
            string[][] numbers = _input.Take(4).Select(x => x.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries)).ToArray();

            var operations = _input[4].Split(' ', StringSplitOptions.RemoveEmptyEntries);
            long sum = 0;
            for(var i = 0; i< operations.Length; i++)
            {

                var first = long.Parse(numbers[0][i]);
                var second = long.Parse(numbers[1][i]);
                var third = long.Parse(numbers[2][i]);
                var fourth = long.Parse(numbers[3][i]);

                sum += operations[i] == "+" ? first + second + third + fourth : first * second * third * fourth;
            }

            return new(sum.ToString());
        }

        public override ValueTask<string> Solve_2()
        {
            for(var i = _input[0].Length; i >= 0; i-= 4)

            return new();
        }
    }
}
