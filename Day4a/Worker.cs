namespace AdventOfCode2025.Day4a;

public class Worker : IWorker
{
    public long DoWork(string inputFile)
    {
        var rolls = new List<string>();
        foreach (var line in File.ReadLines(inputFile))
        {
            if (rolls.Count == 0)
            {
                rolls.Add(string.Concat(Enumerable.Repeat(".", line.Length + 2)));
            }
            rolls.Add("." + line + ".");
        }
        rolls.Add(string.Concat(Enumerable.Repeat(".", rolls[0].Length)));

        var sum = 0L;
        for (var row = 1; row < rolls.Count - 1; row++)
        {
            for (var col = 1; col < rolls[row].Length - 1; col++)
            {
                if (rolls[row][col] == '@' && CountAdjacentRolls(rolls, row, col) < 4)
                {
                    sum++;
                }
            }
        }
        return sum;
    }

    private int CountAdjacentRolls(List<string> rolls, int row, int col)
    {
        var result = 0;
        if (rolls[row - 1][col - 1] == '@') result++;
        if (rolls[row - 1][col] == '@') result++;
        if (rolls[row - 1][col + 1] == '@') result++;
        if (rolls[row][col - 1] == '@') result++;
        if (rolls[row][col + 1] == '@') result++;
        if (rolls[row + 1][col - 1] == '@') result++;
        if (rolls[row + 1][col] == '@') result++;
        if (rolls[row + 1][col + 1] == '@') result++;
        return result;
    }
}
