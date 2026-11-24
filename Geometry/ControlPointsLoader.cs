using System.Globalization;
using System.Numerics;

namespace Projekt2GK.Geometry
{
    public static class ControlPointsLoader
    {
        public const string DefaultFilePath = "Assets/surfaces.txt";
        public static IReadOnlyList<BezierSurface> Load(string path)
        {
            var points = new List<Vector3>();
            string[] lines = File.ReadAllLines(path);

            for (int lineIndex = 0; lineIndex < lines.Length; lineIndex++)
            {
                string line = lines[lineIndex].Trim();
                if (line.Length == 0)
                    continue;

                points.Add(ParsePoint(line, lineIndex + 1));
            }

            if (points.Count == 0)
                throw new FormatException("Plik nie zawiera żadnych punktów kontrolnych.");

            if (points.Count % BezierSurface.ControlPointCount != 0)
                throw new FormatException(
                    $"Liczba punktów kontrolnych ({points.Count}) nie jest wielokrotnością {BezierSurface.ControlPointCount}.");

            var surfaces = new List<BezierSurface>();
            for (int start = 0; start < points.Count; start += BezierSurface.ControlPointCount)
                surfaces.Add(new BezierSurface(points.GetRange(start, BezierSurface.ControlPointCount)));

            return surfaces;
        }

        private static Vector3 ParsePoint(string line, int lineNumber)
        {
            string[] parts = line.Split((char[]?)null, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length != 3)
                throw new FormatException($"Wiersz {lineNumber}: oczekiwano 3 liczb, znaleziono {parts.Length}.");

            var coordinates = new float[3];
            for (int k = 0; k < 3; k++)
            {
                if (!float.TryParse(parts[k], NumberStyles.Float, CultureInfo.InvariantCulture, out coordinates[k]))
                    throw new FormatException($"Wiersz {lineNumber}: \"{parts[k]}\" nie jest poprawną liczbą.");
            }

            return new Vector3(coordinates[0], coordinates[1], coordinates[2]);
        }
    }
}
