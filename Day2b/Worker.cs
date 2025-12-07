namespace AdventOfCode2025.Day2b;

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
                var isValid = true;
                for (var partLength = 1; partLength <= formattedId.Length / 2 && isValid; partLength++)
                {
                    var part = formattedId[..partLength];
                    if (string.Concat(Enumerable.Repeat(part, formattedId.Length / partLength)) == formattedId)
                    {
                        isValid = false;
                    }
                }
                if (!isValid)
                {
                    sum += productId;
                }
            }
        }
        return sum;
    }
}
