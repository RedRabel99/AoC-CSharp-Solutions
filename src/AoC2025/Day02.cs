using Common;

namespace AoC2025;

public class Day02 : BaseLibraryDay
{
    protected override int Year => 2025;
    private string _input;

    public Day02()
    {
        _input = File.ReadAllText(InputFilePath);
    }

    public override ValueTask<string> Solve_1()
    {
        string[] id_ranges = _input.Split(',');
        long sum = 0;


        foreach (var id_range in id_ranges)
        {
            long start = long.Parse(id_range.Split('-')[0]);
            long end = long.Parse(id_range.Split("-")[1]);

            for (var i = start; i <= end; i++)
            {
                string current_id = i.ToString();
                if (current_id.Length % 2 != 0) continue;
                if (current_id[..(current_id.Length / 2)] == current_id[(current_id.Length / 2)..]) sum += i;
            }
        }

        return new(sum.ToString());
    }

    private IEnumerable<long> LongRange(long start, long end)
    {
        while (start <= end)
        {
            yield return start;
            start++;
        }
    }

    public override ValueTask<string> Solve_2()
    {
        string[] id_ranges = _input.Split(',');
        long sum = 0;

        foreach (var id_range in id_ranges)
        {

            long start = long.Parse(id_range.Split('-')[0]);
            long end = long.Parse(id_range.Split('-')[1]);
            for (var i = start; i <= end; i++)
            {
                string current_id = i.ToString();
                //sum += IsInvalidId(current_id) ? long.Parse(current_id) : 0;
                sum += (current_id + current_id).Substring(1, current_id.Length * 2 - 2).Contains(current_id) ? long.Parse(current_id) : 0;
            }
        }

        return new(sum.ToString());
    }

    //Second approach: checking each following substring of given length
    private bool IsInvalidId(string current_id)
    {

        for (int length = 1; length <= current_id.Length - length; length++)
        {
            if (IsRepeated(current_id, 0, length)) return true;
        }
        return false;
    }

    private bool IsRepeated(string text, int start, int length)
    {
        if (text.Length % length != 0) return false;

        while (start < text.Length - length)
        {
            if (text.Substring(start, length) != text.Substring(start + length, length))
            {
                return false;
            }
            start += length;
        }

        return true;
    }
}
