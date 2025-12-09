using System.Text.Json;

namespace AdventOfCode2025.Day4;

public class Day4Part2 : IPuzzle
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
        var iterationResult = 0;
        var map = CreateMap(Lines);

        var iterate = true;
        var maxRows = Lines.Count() - 1;
        var overrideCoords = new Dictionary<string, char> { };

        while (iterate)
        {
            var result = 0;
            var row = 0;
            foreach (var line in Lines)
            {
                for (var col = 0; col < line.Length; col++)
                {
                    var cellContent = map[row + "," + col];
                    if (cellContent == '.')
                    {
                        continue; // Skip
                    }

                    var isAccessible = IsAccessible(row, col, cellContent, map, maxRows, line.Length - 1);
                    if (!isAccessible) continue;

                    overrideCoords.Add(row + "," + col, '.');
                    result++;
                }

                row++;
            }

            if (result > 0)
            {
                iterationResult += result;
                foreach (var key in overrideCoords.Keys.ToList().Where(key => map.ContainsKey(key)))
                {
                    map[key] = overrideCoords[key];
                }
                overrideCoords.Clear();
            }
            else
            {
                iterate = false;
            }
        }

        Console.WriteLine("Answer is " + iterationResult);
        return Task.CompletedTask;
    }

    private static bool IsWithinBounds(int x, int y, int maxRows, int maxCols) =>
        x >= 0 && x <= maxRows && y >= 0 && y <= maxCols;

    private static bool IsAccessible(int x, int y, char c, Dictionary<string, char> map, int maxRows, int maxCols)
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

    private static Dictionary<string, char> CreateMap(IEnumerable<string> lines)
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