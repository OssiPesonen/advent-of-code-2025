namespace AdventOfCode2025.Tools;

public class Sequence
{
    public static bool HasRepeatedNumberSequence(string input)
    {
        if (input.Length % 2 != 0)
            return false;
        
        var len = input.Length / 2;
        var seq = input[..len];
        return (seq + seq) == input;
    }
}