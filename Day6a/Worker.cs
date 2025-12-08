namespace AdventOfCode2025.Day6a;

public class Worker : IWorker
{
    public long DoWork(string inputFile)
    {
        var values = new List<long[]>();
        string[] operators = [];
        foreach (var line in File.ReadLines(inputFile))
        {
            var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            try
            {
                values.Add(parts.Select(long.Parse).ToArray());
            }
            catch
            {
                operators = parts;
            }
        }

        var sum = 0L;
        for (var i = 0; i < operators.Length; i++)
        {
            var op = operators[i];
            var result = op == "+" ? 0L : 1L;
            for (var j = 0; j < values.Count; j++)
            {
                result = op == "+" ? result + values[j][i] : result * values[j][i];
            }
            sum += result;
        }

        return sum;
    }
}
