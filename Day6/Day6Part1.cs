namespace AdventOfCode2025.Day5;

public class Day6Part1 : IPuzzle
{
    private static IEnumerable<string> Lines => File.ReadLines("input.txt");

    public Task RunAsync()
    {
        long result = 0;
        var row = 0;
        var maths = new Dictionary<int, List<long>>();
        var operators = new Dictionary<int, string>();
        
        foreach (var line in Lines)
        {
            var l = line.Trim();
            
            if (row == Lines.Count() - 1)
            {
                var col = 0;
                foreach (var op in l.Split(' '))
                {
                    // Skip extra spaces
                    if (op == "") continue;
                    operators[col] = op;
                    col++;
                }
            }
            else
            {
                var col = 0;
                foreach (var num in l.Split(' '))
                {
                    // Skip extra spaces
                    if (num == "") continue;
                    
                    if (maths.ContainsKey(col))
                    {
                        maths[col].Add(long.Parse(num));
                    }
                    else
                    {
                        maths.Add(col, [long.Parse(num)]);
                    }

                    col++;
                }
            }

            row++;
        }
        
        foreach (var col in maths.Keys)
        {
            var op = operators[col];
            
            if (op == "*")
            {
                result += maths[col].Aggregate((a, b) => a * b);
            }

            if (op == "+")
            {
                result += maths[col].Sum();
            }
        }

        Console.WriteLine("Answer is " + result);
        return Task.CompletedTask;
    }

}