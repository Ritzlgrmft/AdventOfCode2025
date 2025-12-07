namespace AdventOfCode2025.Day7a;

public class Worker : IWorker
{
    public long DoWork(string inputFile)
    {
        var lines = File.ReadLines(inputFile);
        var beams = new List<int>() { lines.First().IndexOf('S') };

        var splitted = 0L;
        foreach (var line in lines.Skip(1))
        {
            var newBeams = new List<int>();
            foreach (var beam in beams)
            {
                if (line[beam] == '.')
                {
                    newBeams.Add(beam);
                }
                else if (line[beam] == '^')
                {
                    newBeams.Add(beam - 1);
                    newBeams.Add(beam + 1);
                    splitted++;
                }
            }

            beams = newBeams.Distinct().ToList();
        }

        return splitted;
    }
}
