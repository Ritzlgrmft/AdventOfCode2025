namespace AdventOfCode2025.Day12a;

public class Worker : IWorker
{
    private readonly List<(string[] shape, List<string[]> versions)> shapes = [];
    private readonly List<(int width, int height, int[] numberOfShapes)> regions = [];

    public long DoWork(string inputFile)
    {
        var lines = File.ReadLines(inputFile).ToList();
        var index = 0;
        while (index < lines.Count)
        {
            if (!lines[index].Contains('x'))
            {
                index++;
                var shape = new List<string>();
                while (index < lines.Count && !string.IsNullOrEmpty(lines[index]))
                {
                    shape.Add(lines[index]);
                    index++;
                }
                shapes.Add((shape: [.. shape], versions: []));
            }
            else
            {
                var lineParts = lines[index]
                    .Split("x: ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();
                var region = (width: lineParts[0], height: lineParts[1], numberOfShapes: lineParts.Skip(2).ToArray());
                regions.Add(region);
            }
            index++;
        }

        foreach (var shape in shapes)
        {
            var versions = new List<string[]>();
            var shapeToAdd = shape.shape;
            for (var i = 0; i < 4; i++)
            {
                AddVersionIfNotExists(versions, shapeToAdd);
                shapeToAdd = RotateShape(shapeToAdd);
            }
            shapeToAdd = FlipShape(shape.shape);
            for (var i = 0; i < 4; i++)
            {
                AddVersionIfNotExists(versions, shapeToAdd);
                shapeToAdd = RotateShape(shapeToAdd);
            }
            shape.versions.AddRange(versions);
        }

        var sum = 0L;
        foreach (var (width, height, numberOfShapes) in regions)
        {
            var area = new bool[height, width];
            var placesToFill = 0L;
            for (var i = 0; i < shapes.Count; i++)
            {
                placesToFill += numberOfShapes[i] * shapes[i].shape.Sum(row => row.Count(c => c == '#'));
            }
            var emptyPlaces = width * height - placesToFill;
            if (emptyPlaces >= 0)
            {
                if (CanShapesBePlaced(area, numberOfShapes))
                {
                    sum++;
                }
            }
        }

        return sum;
    }

    private void AddVersionIfNotExists(List<string[]> versions, string[] shapeToAdd)
    {
        if (!versions.Any(v => Enumerable.SequenceEqual(v, shapeToAdd)))
        {
            versions.Add(shapeToAdd);
        }
    }

    private string[] RotateShape(string[] shape)
    {
        var result = new string[shape[0].Length];
        var newHeight = shape[0].Length;
        int newWidth = shape.Length;
        for (var newRow = 0; newRow < newHeight; newRow++)
        {
            var newRowChars = new char[newWidth];
            for (var newCol = 0; newCol < shape.Length; newCol++)
            {
                newRowChars[newCol] = shape[shape.Length - 1 - newCol][newRow];
            }
            result[newRow] = new string(newRowChars);
        }
        return result;
    }

    private string[] FlipShape(string[] shape)
    {
        return shape
            .Select(row => new string(row.Reverse().ToArray()))
            .ToArray();
    }

    private bool CanShapesBePlaced(bool[,] area, int[] numberOfShapes)
    {
        while (numberOfShapes.Any(n => n > 0))
        {
            var shapeIndex = numberOfShapes
                .Select((n, i) => (n, i))
                .Where(t => t.n > 0)
                .Select(t => t.i)
                .First();

            var shape = shapes[shapeIndex];
            foreach (var version in shape.versions)
            {
                var shapeHeight = version.Length;
                var shapeWidth = version[0].Length;
                for (var row = 0; row <= area.GetLength(0) - shapeHeight; row++)
                {
                    for (var col = 0; col <= area.GetLength(1) - shapeWidth; col++)
                    {
                        if (CanPlaceShapeAt(area, version, row, col))
                        {
                            var areaClone = (bool[,])area.Clone();
                            PlaceShapeAt(areaClone, version, row, col);
                            var numberOfShapesClone = (int[])numberOfShapes.Clone();
                            numberOfShapesClone[shapeIndex]--;
                            if (CanShapesBePlaced(areaClone, numberOfShapesClone))
                            {
                                return true;
                            }
                        }
                    }

                }
            }
            return false;
        }
        return true;
    }

    private bool CanPlaceShapeAt(bool[,] area, string[] version, int row, int col)
    {
        for (var r = 0; r < version.Length; r++)
        {
            for (var c = 0; c < version[0].Length; c++)
            {
                if (version[r][c] == '#' && area[row + r, col + c])
                {
                    return false;
                }
            }
        }
        return true;
    }

    private void PlaceShapeAt(bool[,] area, string[] version, int row, int col)
    {
        for (var r = 0; r < version.Length; r++)
        {
            for (var c = 0; c < version[0].Length; c++)
            {
                if (version[r][c] == '#')
                {
                    area[row + r, col + c] = true;
                }
            }
        }
    }

}
