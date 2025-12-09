namespace AdventOfCode2025.Day4;

public class Day4Part1 : IPuzzle
{
    private static IEnumerable<string> Lines => File.ReadLines("input.txt");

    private static readonly (int dx, int dy)[] Directions =
    {
        (-1, -1), // top-left
        (-1, 0), // top
        (-1, 1), // top-right
        (0, -1), // left
        (0, 1), // right
        (1, -1), // bottom-left
        (1, 0), // bottom
        (1, 1) // bottom-right
    };

    public Task RunAsync()
    {
        var result = 0;
        var map = CreateMap(Lines);

        var row = 0;
        var maxRows = Lines.Count() - 1;
        foreach (var line in Lines)
        {
            for (var col = 0; col < line.Length; col++)
            {
                if (line[col] == '.')
                {
                    continue; // Skip
                }

                var isAccessible = IsAccessible(row, col, line[col], map, maxRows, line.Length - 1);
                if (isAccessible)
                {
                    result++;
                }
            }

            row++;
        }

        Console.WriteLine("Answer is " + result);
        return Task.CompletedTask;
    }

    private bool IsWithinBounds(int x, int y, int maxRows, int maxCols) => x >= 0 && x <= maxRows && y >= 0 && y <= maxCols;

    private bool IsAccessible(int x, int y, char c, Dictionary<string, char> map, int maxRows, int maxCols)
    {
        var paperRollsNextToCell = 0;
        foreach (var (dx, dy) in Directions)
        {
            int nx = x + dx;
            int ny = y + dy;
            
            if (IsWithinBounds(nx, ny, maxRows, maxCols) && map[nx + "," + ny] == '@')
            {
                paperRollsNextToCell++;
            }
        }

        return paperRollsNextToCell < 4;
    }

    private Dictionary<string, char> CreateMap(IEnumerable<string> lines)
    {
        var map = new Dictionary<string, char>();

        var row = 0;
        foreach (var line in lines)
        {
            for (var col = 0; col < line.Length; col++)
            {
                var coords = row + "," + col;
                map.Add(coords, line[col]);
            }

            row++;
        }
        
        return map;
    }
}