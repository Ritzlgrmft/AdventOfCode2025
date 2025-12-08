namespace AdventOfCode2025.Day8b;

public class Worker : IWorker
{
    public long DoWork(string inputFile)
    {
        var boxes = File.ReadLines(inputFile)
            .Select(l => l.Split(","))
            .Select(p => (x: Int32.Parse(p[0]), y: Int32.Parse(p[1]), z: Int32.Parse(p[2])))
            .ToList();
        var circuits = new List<List<int>>();

        var distances = new List<(double distance, int box1, int box2)>();
        for (var i = 0; i < boxes.Count; i++)
        {
            for (var j = i + 1; j < boxes.Count; j++)
            {
                var box1 = boxes[i];
                var box2 = boxes[j];
                var distance = Math.Pow(box1.x - box2.x, 2) + Math.Pow(box1.y - box2.y, 2) + Math.Pow(box1.z - box2.z, 2);
                distances.Add((distance, i, j));
            }
        }
        distances = distances
            .OrderBy(d => d.distance)
            .ToList();

        foreach (var (distance, box1, box2) in distances)
        {
            var circuit1 = circuits.FirstOrDefault(c => c.Contains(box1));
            var circuit2 = circuits.FirstOrDefault(c => c.Contains(box2));
            if (circuit1 == null && circuit2 == null)
            {
                circuits.Add([box1, box2]);
            }
            else if (circuit1 != null && circuit2 == null)
            {
                circuit1.Add(box2);
            }
            else if (circuit1 == null && circuit2 != null)
            {
                circuit2.Add(box1);
            }
            else if (circuit1 != circuit2)
            {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8604 // Possible null reference argument.
                circuit1.AddRange(circuit2);
#pragma warning restore CS8604 // Possible null reference argument.
#pragma warning restore CS8602 // Dereference of a possibly null reference.
                circuits.Remove(circuit2);
            }
            if (circuits.Count == 1 && circuits[0].Count == boxes.Count)
            {
                return ((long)boxes[box1].x) * boxes[box2].x;
            }
        }
        return -1;
    }
}
