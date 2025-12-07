
namespace AdventOfCode2025.Day7b;

public class Worker : IWorker
{
    public long DoWork(string inputFile)
    {
        var lines = File.ReadLines(inputFile);
        var beams = new List<Beam>() { new() { Pos = lines.First().IndexOf('S'), Count = 1 } };

        var splitted = 0L;
        foreach (var line in lines.Skip(1))
        {
            var newBeams = new List<Beam>();
            foreach (var beam in beams)
            {
                if (line[beam.Pos] == '.')
                {
                    AddBeam(newBeams, beam.Pos, beam.Count);
                }
                else if (line[beam.Pos] == '^')
                {
                    AddBeam(newBeams, beam.Pos - 1, beam.Count);
                    AddBeam(newBeams, beam.Pos + 1, beam.Count);
                    splitted++;
                }
            }

            beams = newBeams;
        }

        return beams.Sum(b => b.Count);
    }

    private void AddBeam(List<Beam> newBeams, int pos, long count)
    {
        var beam = newBeams.FirstOrDefault(b => b.Pos == pos);
        if (beam != null)
        {
            beam.Count += count;
        }
        else
        {
            newBeams.Add(new Beam { Pos = pos, Count = count });
        }
    }

    private class Beam
    {
        public int Pos { get; set; }
        public long Count { get; set; }
        public override string ToString() => $"Pos: {Pos}, Count: {Count}";
    }
}
