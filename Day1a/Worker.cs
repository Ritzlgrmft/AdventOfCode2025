namespace AdventOfCode2025.Day1a;

public class Worker : IWorker
{
    public long DoWork(string inputFile)
    {
        var rotations = new List<int>();
        foreach (var line in File.ReadLines(inputFile))
        {
            if (line.StartsWith("L"))
            {
                rotations.Add(100 - int.Parse(line[1..]));
            }
            else if (line.StartsWith("R"))
            {
                rotations.Add(int.Parse(line[1..]));
            }
        }

        var dial = 50;
        var zeroCount = 0L;
        foreach (var rotation in rotations)
        {
            dial = (dial + rotation) % 100;
            if (dial == 0)
            {
                zeroCount++;
            }
        }
        return zeroCount;
    }
}
