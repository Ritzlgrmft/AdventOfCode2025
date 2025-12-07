namespace AdventOfCode2025.Day5b;

public class Worker : IWorker
{
    public long DoWork(string inputFile)
    {
        var freshIngredients = new List<(long min, long max)>();
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
        }

        bool ingredientsConsolidated;
        do
        {
            ingredientsConsolidated = false;
            for (var i = 0; i < freshIngredients.Count && !ingredientsConsolidated; i++)
            {
                for (var j = i + 1; j < freshIngredients.Count && !ingredientsConsolidated; j++)
                {
                    var first = freshIngredients[i];
                    var second = freshIngredients[j];
                    if (first.max >= second.min && second.max >= first.min)
                    {
                        var newIngredient = (min: Math.Min(first.min, second.min), max: Math.Max(first.max, second.max));
                        freshIngredients.RemoveAt(j);
                        freshIngredients.RemoveAt(i);
                        freshIngredients.Add(newIngredient);
                        ingredientsConsolidated = true;
                    }
                }
            }

        } while (ingredientsConsolidated);

        var sum = freshIngredients.Sum(ingredient => ingredient.max - ingredient.min + 1);
        return sum;
    }
}
