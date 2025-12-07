using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;

namespace AdventOfCode2025.Day3b;

public class Worker : IWorker
{
    public long DoWork(string inputFile)
    {
        const int numberOfBatteries = 12;

        var banks = File.ReadLines(inputFile);
        var sum = 0L;
        foreach (var bank in banks)
        {
            var joltage = 0L;
            var remainingBank = bank;
            for (var battery = 1; battery <= numberOfBatteries; battery++)
            {
                var biggestValue = remainingBank.Substring(0, remainingBank.Length - numberOfBatteries + battery).Max();
                joltage = joltage * 10 + (biggestValue - '0');
                remainingBank = remainingBank.Substring(remainingBank.IndexOf(biggestValue) + 1);
            }

            sum += joltage;
        }
        return sum;
    }
}
