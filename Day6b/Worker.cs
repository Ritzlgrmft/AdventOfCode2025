namespace AdventOfCode2025.Day6b;

public class Worker : IWorker
{
    public long DoWork(string inputFile)
    {
        var data = File.ReadLines(inputFile).ToArray();
        var operators = data.Last();

        var sum = 0L;
        var op = 'x';
        var result = 0L;
        for (var col = 0; col < operators.Length; col++)
        {
            if (operators[col] != ' ')
            {
                op = operators[col];
                sum += result;
                result = op == '+' ? 0L : 1L;
            }

            var value = 0L;
            for (var row = 0; row < data.Length - 1; row++)
            {
                if (data[row][col] != ' ')
                {
                    value = value * 10 + (data[row][col] - '0');
                }
            }

            if (value > 0)
            {
                result = op == '+' ? result + value : result * value;
            }
        }
        sum += result;

        return sum;
    }
}
