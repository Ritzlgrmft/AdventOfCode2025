namespace AdventOfCode2025.Day10a;

public class Worker : IWorker
{
    public long DoWork(string inputFile)
    {
        var targets = new List<string>();
        var buttons = new List<int[][]>();
        foreach (var line in File.ReadLines(inputFile))
        {
            var parts = line.Split(" (){}[]".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            targets.Add(parts[0]);
            buttons.Add(parts
                .Skip(1)
                .SkipLast(1)
                .Select(p => p.Split(',').Select(int.Parse).ToArray())
                .ToArray());
        }

        var sum = 0L;
        for (var machine = 0; machine < targets.Count; machine++)
        {
            sum += PressButtons(targets[machine], buttons[machine]);
        }
        return sum;
    }

    private long PressButtons(string target, int[][] buttonSet)
    {
        var presses = 0L;
        var states = new List<string>() { target.Replace('#', '.') };
        while (true)
        {
            presses++;
            var newStates = new List<string>();
            foreach (var state in states)
            {
                foreach (var button in buttonSet)
                {
                    var newState = pressButton(state, button);
                    if (target == newState)
                    {
                        return presses;
                    }
                    newStates.Add(newState);
                }
            }
            states = newStates;
        }
    }

    private string pressButton(string state, int[] button)
    {
        var result = state.ToCharArray();
        foreach (var index in button)
        {
            result[index] = result[index] == '#' ? '.' : '#';
        }
        return new string(result);
    }
}