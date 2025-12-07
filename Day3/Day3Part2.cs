namespace AdventOfCode2025.Day3;

public class Day3Part2 : IPuzzle
{
    private static IEnumerable<string> Lines => File.ReadLines("input.txt");

    public Task RunAsync()
    {
        long result = 0;
        const int maxLen = 12;
        foreach (var line in Lines)
        {
            var toRemove = line.Length - maxLen;
            var stack = new Stack<char>();

            // Go through each digit and keep the highest one on top of the stack
            foreach (var c in line)
            {
                while (stack.Count > 0 && toRemove > 0 && stack.Peek() < c)
                {
                    stack.Pop();
                    toRemove--;
                }

                stack.Push(c);
            }
            
            var number = stack.Reverse().Take(maxLen);
            result += long.Parse(string.Join("", number));
        }
        
        Console.WriteLine("Answer is " + result);
        return Task.CompletedTask;
    }
}