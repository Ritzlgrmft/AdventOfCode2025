using Microsoft.Z3;

namespace AdventOfCode2025.Day10b;

public class Worker : IWorker
{
    public long DoWork(string inputFile)
    {
        var buttons = new List<int[][]>();
        var joltages = new List<int[]>();
        foreach (var line in File.ReadLines(inputFile))
        {
            var parts = line.Split(" (){}[]".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            buttons.Add(parts
                .Skip(1)
                .SkipLast(1)
                .Select(p => p.Split(',').Select(int.Parse).ToArray())
                .ToArray());
            joltages.Add(parts
                .Last()
                .Split(',')
                .Select(int.Parse)
                .ToArray());
        }

        var sum = 0L;
        for (var machine = 0; machine < joltages.Count; machine++)
        {
            var presses = PressButtons(joltages[machine], buttons[machine]);
            sum += presses;
        }
        return sum;
    }

    private long PressButtons(int[] joltages, int[][] buttonSet)
    {
        using var ctx = new Context();
        using var opt = ctx.MkOptimize();

        // define variables
        var presses = Enumerable.Range(0, buttonSet.Length)
            .Select(i => ctx.MkIntConst($"x{i}"))
            .ToArray();

        // initialize variables
        foreach (var press in presses)
            opt.Add(ctx.MkGe(press, ctx.MkInt(0)));

        // add linear equations
        for (int i = 0; i < joltages.Length; i++)
        {
            var affecting = presses.Where((_, j) => buttonSet[j].Contains(i)).ToArray();
            if (affecting.Length > 0)
            {
                opt.Add(ctx.MkEq(ctx.MkAdd(affecting), ctx.MkInt(joltages[i])));
            }
        }

        // evaluate equations
        opt.MkMinimize(ctx.MkAdd(presses));
        opt.Check();
        return presses.Sum(p => ((IntNum)opt.Model.Evaluate(p, true)).Int64);
    }
}