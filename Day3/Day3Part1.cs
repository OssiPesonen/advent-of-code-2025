namespace AdventOfCode2025.Day3;

public class Day3Part1 : IPuzzle
{
    private static IEnumerable<string> Lines => File.ReadLines("input.txt");
    
    public Task RunAsync()
    {
        var result = 0;
        foreach (var line in Lines)
        {
            var largestNumber = 0;
            for (var i = 0; i < line.Length; i++)
            {
                var firstNumber = line[i];
                for (var k = 1 + i; k < line.Length; k++) {
                    var secondNumber = line[k];
                    var largestBatteries = int.Parse(firstNumber + secondNumber.ToString());
                    if (largestBatteries > largestNumber) largestNumber = largestBatteries;
                }
            }
            result += largestNumber;
        }
        
        Console.WriteLine("Answer is " + result);
        return Task.CompletedTask;
    }
}