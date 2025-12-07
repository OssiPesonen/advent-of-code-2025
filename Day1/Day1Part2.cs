namespace AdventOfCode2025.Day1;

public class Day1Part2 : Puzzle
{
    private static IEnumerable<string> Lines => File.ReadLines("input.txt");

    public override Task RunAsync()
    { 
        const int startingPoint = 50;

        var numberOfTimesPastZero = 0;
        var positionOnDial = startingPoint;

        foreach (var line in Lines)
        {
            var dir = line[0];
            var steps = int.Parse(line[1..]);
            for (var i = 0; i < steps; i++)
            {
                // This is not optimal, but it works
                positionOnDial += dir == 'L' ? -1 : 1;
                
                // Left, needs to be checked before we adjust dial loop
                if (dir == 'L' && positionOnDial == 0) numberOfTimesPastZero++;
                
                positionOnDial = positionOnDial switch
                {
                    -1 => 99,
                    100 => 0,
                    _ => positionOnDial
                };
                
                // Right needs to be checked after we adjust dial loop
                if (dir == 'R' && positionOnDial == 0) numberOfTimesPastZero++;
            }
        }

        Console.WriteLine("Answer is " + numberOfTimesPastZero + "");
        return Task.CompletedTask;
    }
}