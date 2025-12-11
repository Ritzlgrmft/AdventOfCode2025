namespace AdventOfCode2025.Day11a;

public class Worker : IWorker
{
    public long DoWork(string inputFile)
    {
        var connections = new Dictionary<string, string[]>();
        foreach (var line in File.ReadLines(inputFile))
        {
            var parts = line.Split(" :".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            connections[parts[0]] = parts.Skip(1).ToArray();
        }

        return CountConnections(connections, "you"); ;
    }

    private long CountConnections(Dictionary<string, string[]> connections, string device)
    {
        if (device == "out")
        {
            return 1;
        }

        var total = 0L;
        foreach (var next in connections[device])
        {
            total += CountConnections(connections, next);
        }
        return total;
    }
}