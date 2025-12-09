namespace AdventOfCode2025.Day5;

public class Day5Part2 : IPuzzle
{
    private static IEnumerable<string> Lines => File.ReadLines("input.txt");

    private class Range(long start, long end)
    {
        public long Start { get; set; } = start;
        public long End { get; set; } = end;
    }

    public Task RunAsync()
    {
        var ranges = new List<Range>();

        foreach (var line in Lines)
        {
            if (line == string.Empty)
            {
                break;
            }

            var values = line.Split('-');
            var num1 = long.Parse(values[0]);
            var num2 = long.Parse(values[1]);
            ranges.Add(new Range(num1, num2));
        }

        // Merge ranges for overlaps or adjacent values
        var merged = MergeRanges(ranges);
        var result = merged.Sum(r => r.End - r.Start + 1);

        Console.WriteLine("Answer is " + result);
        return Task.CompletedTask;
    }

    private static List<Range> MergeRanges(List<Range> ranges)
    {
        // Sort based on range starting value
        ranges.Sort((a, b) => a.Start.CompareTo(b.Start));

        var merged = new List<Range>();
        var current = ranges[0];

        foreach (var r in ranges.Skip(1))
        {
            if (r.Start <= current.End + 1)
            {
                // Overlapping or adjacent
                current.End = Math.Max(current.End, r.End);
            }
            else
            {
                merged.Add(current);
                current = r;
            }
        }

        merged.Add(current);
        return merged;
    }
}