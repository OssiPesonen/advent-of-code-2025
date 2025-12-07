namespace AdventOfCode2025.Day2;

public class Day2Part1 : IPuzzle
{
    private static IEnumerable<string> Lines => File.ReadLines("input.txt");
    
    public Task RunAsync()
    {
        var productIds = Lines.First().Split(',');
        List<long> invalidIds = [];
        foreach (var id in productIds)
        {
            var split = id.Split('-');
            var start = long.Parse(split[0]);
            var end = long.Parse(split[1]);

            for (var i = start; i <= end; i++)
            {
                if (Tools.Sequence.HasRepeatedNumberSequence(i.ToString()))
                {
                    invalidIds.Add(i);
                }
            }
        }
        
        Console.WriteLine("Answer is " + invalidIds.Sum());
        return Task.CompletedTask;
    }
}