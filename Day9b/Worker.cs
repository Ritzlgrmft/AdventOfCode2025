using NetTopologySuite.Geometries;

namespace AdventOfCode2025.Day9b;

public class Worker : IWorker
{
    public long DoWork(string inputFile)
    {
        var gf = NetTopologySuite.NtsGeometryServices.Instance.CreateGeometryFactory(4326);

        var redTiles = File.ReadLines(inputFile)
            .Select(line => line.Split(","))
            .Select(parts => (row: int.Parse(parts[1]), col: int.Parse(parts[0])))
            .ToList();

        var coordinates = redTiles.Select(t => new Coordinate(t.col, t.row)).ToList();
        coordinates.Add(coordinates[0]);
        var bounds = gf.CreatePolygon(coordinates.ToArray());

        var squares = new List<(int row1, int col1, int row2, int col2, long size)>();
        for (var i = 0; i < redTiles.Count; i++)
        {
            for (var j = i + 1; j < redTiles.Count; j++)
            {
                var (row1, col1) = redTiles[i];
                var (row2, col2) = redTiles[j];

                var square = gf.CreatePolygon([
                    new Coordinate(col1, row1),
                    new Coordinate(col1, row2),
                    new Coordinate(col2, row2),
                    new Coordinate(col2, row1),
                    new Coordinate(col1, row1)
                ]);
                if (bounds.Contains(square))
                {
                    var size = (Math.Abs(row2 - row1) + 1L) * (Math.Abs(col2 - col1) + 1L);
                    squares.Add((row1, col1, row2, col2, size));
                }
            }
        }

        return squares.Max(sq => sq.size);
    }
}