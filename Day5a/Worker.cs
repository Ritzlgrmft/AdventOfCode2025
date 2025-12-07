namespace AdventOfCode2025.Day5a;

public class Worker : IWorker
{
    public long DoWork(string inputFile)
    {
        var freshIngredients = new List<(long min, long max)>();
        var availableIngredients = new List<long>();
        var readFreshIngredients = true;
        foreach (var line in File.ReadLines(inputFile))
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                readFreshIngredients = false;
            }
            else if (readFreshIngredients)
            {
                var parts = line.Split('-');
                freshIngredients.Add((long.Parse(parts[0]), long.Parse(parts[1])));
            }
            else
            {
                availableIngredients.Add(long.Parse(line));
            }
        }

        var sum = 0L;
        foreach (var ingredient in availableIngredients)
        {
            var isFresh = false;
            foreach (var freshIngredient in freshIngredients)
            {
                if (ingredient >= freshIngredient.min && ingredient <= freshIngredient.max)
                {
                    isFresh = true;
                    break;
                }
            }
            if (isFresh)
            {
                sum++;
            }
        }

        return sum;
    }
}
