namespace AdventOfCode2025.Day11b;

public class Worker : IWorker
{
    readonly Dictionary<string, string[]> connections = [];
    readonly Dictionary<(string device, string target), long> cache = [];

    public long DoWork(string inputFile)
    {
        foreach (var line in File.ReadLines(inputFile))
        {
            var parts = line.Split(" :".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            connections[parts[0]] = parts.Skip(1).ToArray();
        }

        // in our data, there are no connections from dac via fft to out
        // therefore we can just count the connections separately and multiply
        // however, the real performance boost was caching the intermediate results
        var connectionsFromDacToOut = CountConnections("dac", "out");
        var connectionsFromFftToDac = CountConnections("fft", "dac");
        var connectionsFromSvrToFft = CountConnections("svr", "fft");
        return connectionsFromDacToOut * connectionsFromFftToDac * connectionsFromSvrToFft;
    }

    private long CountConnections(string device, string target)
    {
        if (device == target)
        {
            return 1;
        }
        else if (!connections.ContainsKey(device))
        {
            return 0;
        }
        else if (cache.ContainsKey((device, target)))
        {
            return cache[(device, target)];
        }

        var total = 0L;
        foreach (var next in connections[device])
        {
            total += CountConnections(next, target);
        }
        cache[(device, target)] = total;
        return total;
    }

}