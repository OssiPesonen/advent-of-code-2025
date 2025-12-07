// app.cs

const string input = "input.txt";
if (!File.Exists(input))
    throw new Exception(
        $"Input file was not found at project root. Please create input.txt and add your puzzle input there.");
var lines = File.ReadLines(input);

const int right = 1;
const int left = -1;
const int startingPoint = 50;

var numberOfTimesPastZero = 0;
var positionOnDial = startingPoint;

foreach (var line in lines)
{
    var dir = line.Substring(0, 1);
    var steps = Int64.Parse(line.Remove(0, 1));
    for (var i = 0; i < steps; i++)
    {
        positionOnDial += dir == "L" ? left : right;
        positionOnDial = positionOnDial switch
        {
            -1 => 99,
            100 => 0,
            _ => positionOnDial
        };
    }
    
    if (positionOnDial == 0) numberOfTimesPastZero++;
}

Console.WriteLine("Answer is " + numberOfTimesPastZero + "");