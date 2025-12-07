namespace AdventOfCode2025.Day1b;

public class Worker : IWorker
{
    public long DoWork(string inputFile)
    {
        var directions = new List<char>();
        var rotations = new List<int>();
        foreach (var line in File.ReadLines(inputFile))
        {
            directions.Add(line[0]);
            rotations.Add(int.Parse(line[1..]));
        }

        var dial = 50;
        var zeroCount = 0L;
        for (var i = 0; i < rotations.Count; i++)
        {
            var direction = directions[i];
            var rotation = rotations[i];

            zeroCount += rotation / 100;
            rotation = rotation % 100;

            if (direction == 'L')
            {
                if (dial == 0)
                {
                    dial += 100 - rotation;
                }
                else
                {
                    dial -= rotation;
                    if (dial < 0)
                    {
                        dial += 100;
                        zeroCount++;
                    }
                }
            }
            else if (direction == 'R')
            {
                dial += rotation;
                if (dial == 100)
                {
                    dial -= 100;
                }
                else if (dial > 100)
                {
                    dial -= 100;
                    zeroCount++;
                }
            }
            if (dial == 0)
            {
                zeroCount++;
            }
        }
        return zeroCount;
    }
}
