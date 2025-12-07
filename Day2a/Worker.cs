namespace AdventOfCode2025.Day2a;

public class Worker : IWorker
{
    public long DoWork(string inputFile)
    {
        var rangesLeft = new List<long>();
        var rangesRight = new List<long>();
        foreach (var line in File.ReadLines(inputFile))
        {
            foreach (var part in line.Split(',', StringSplitOptions.RemoveEmptyEntries))
            {
                var ranges = part.Split('-', StringSplitOptions.RemoveEmptyEntries);
                rangesLeft.Add(long.Parse(ranges[0]));
                rangesRight.Add(long.Parse(ranges[1]));
            }
        }

        var sum = 0L;
        for (var i = 0; i < rangesLeft.Count; i++)
        {
            for (var productId = rangesLeft[i]; productId <= rangesRight[i]; productId++)
            {
                var formattedId = productId.ToString();
                if (formattedId.Length % 2 == 0)
                {
                    var halfLength = formattedId.Length / 2;
                    var leftPart = formattedId[..halfLength];
                    var rightPart = formattedId[halfLength..];
                    if (leftPart == rightPart)
                    {
                        sum += productId;
                    }
                }
            }
        }
        return sum;
    }
}
