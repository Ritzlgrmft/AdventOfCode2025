namespace AdventOfCode2025.Day9a;

public class Worker : IWorker
{
    public long DoWork(string inputFile)
    {
        var redTiles = File.ReadLines(inputFile)
            .Select(line => line.Split(","))
            .Select(parts => (row: int.Parse(parts[1]), col: int.Parse(parts[0])))
            .ToList();

        var squares = new List<(int row1, int col1, int row2, int col2, long size)>();
        for (var i = 0; i < redTiles.Count; i++)
        {
            for (var j = i + 1; j < redTiles.Count; j++)
            {
                var tile1 = redTiles[i];
                var tile2 = redTiles[j];

                var size = (Math.Abs(tile2.row - tile1.row) + 1L) * (Math.Abs(tile2.col - tile1.col) + 1L);
                squares.Add((tile1.row, tile1.col, tile2.row, tile2.col, size));
            }
        }

        return squares.Max(sq => sq.size);
    }
}