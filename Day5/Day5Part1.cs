using AdventOfCode2025.Tools.IntervalTree;

namespace AdventOfCode2025.Day5;

public class Day5Part1 : IPuzzle
{
    private static IEnumerable<string> Lines => File.ReadLines("input.txt");

    public Task RunAsync()
    {
        var result = 0;
        var isRange = true;
        var tree = new IntervalTree<long, string>();

        var row = 0;
        foreach (var line in Lines)
        {
            if (line == string.Empty)
            {
                isRange = false;
                continue;
            }
            
            if (isRange)
            {
                var values = line.Split('-');
                tree.Add(long.Parse(values[0]), long.Parse(values[1]), row.ToString());
            }
            else
            {
                var isFresh = tree.Query(long.Parse(line));
                if (isFresh.Any())
                {
                    result++;
                }
            }

            row++;
        }

        Console.WriteLine("Answer is " + result);
        return Task.CompletedTask;
    }

}