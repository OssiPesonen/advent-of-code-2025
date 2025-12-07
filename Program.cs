namespace AdventOfCode2025;

using System.Reflection;

class Program
{
    static async Task Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Usage: dotnet run -- --puzzle <name>");
            return;
        }

        var puzzleName = args[1].ToLower();

        var puzzles = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(t => !t.IsAbstract && typeof(Puzzle).IsAssignableFrom(t))
            .Select(t => (Puzzle)Activator.CreateInstance(t)!)
            .ToList();

        var puzzle = puzzles.FirstOrDefault(p => p.GetType().ToString().Split('.').Last().Equals(puzzleName, StringComparison.CurrentCultureIgnoreCase));

        if (puzzle == null)
        {
            Console.WriteLine($"Puzzle '{puzzleName}' not found.");
            Console.WriteLine("Available puzzles:");
            foreach (var p in puzzles) Console.WriteLine("  " + p.GetType().ToString().Split('.').Last().ToLower());
            return;
        }

        await puzzle.RunAsync();
    }
}