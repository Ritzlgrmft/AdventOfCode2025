using System.Diagnostics.CodeAnalysis;

namespace AdventOfCode2025.Day3a;

public class Worker : IWorker
{
    public long DoWork(string inputFile)
    {
        var banks = File.ReadLines(inputFile);
        var sum = 0L;
        foreach (var bank in banks)
        {
            var joltage1 = bank.Substring(0, bank.Length - 1).Max();
            var joltage2 = bank.Substring(bank.IndexOf(joltage1) + 1).Max();
            var joltage = (joltage1 - '0') * 10 + (joltage2 - '0');
            sum += joltage;
        }
        return sum;
    }
}
