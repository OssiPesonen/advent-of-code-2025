namespace AdventOfCode2025.Day1;

public class Day1Part1 : Puzzle
{
    private static IEnumerable<string> Lines => File.ReadLines("input.txt");
    
    public override Task RunAsync()
    {
        var numberOfTimesPastZero = 0;
        var positionOnDial = 50;

        foreach (var line in Lines)
        {
            var dir = line[0] == 'L' ? -1 : 1;
            var distance = int.Parse(line[1..]);

            positionOnDial += dir * distance;
            positionOnDial %= 100;
            
            if (positionOnDial == 0) numberOfTimesPastZero++;
        }

        Console.WriteLine("Answer is " + numberOfTimesPastZero + "");
        return Task.CompletedTask;
    }
    
}