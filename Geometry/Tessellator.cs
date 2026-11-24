namespace Projekt2GK.Geometry
{
    public static class Tessellator
    {
        public static Mesh Tessellate(BezierSurface surface, int steps)
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(steps, 1);

            int pointsPerSide = steps + 1;
            var vertices = new Vertex[pointsPerSide * pointsPerSide];
            for (int i = 0; i < pointsPerSide; i++)
            {
                float u = (float)i / steps;
                for (int j = 0; j < pointsPerSide; j++)
                {
                    float v = (float)j / steps;
                    vertices[Index(i, j)] = new Vertex(surface.Evaluate(u, v), u, v);
                }
            }

            var triangles = new Triangle[2 * steps * steps];
            int t = 0;
            for (int i = 0; i < steps; i++)
            {
                for (int j = 0; j < steps; j++)
                {
                    Vertex v00 = vertices[Index(i, j)];
                    Vertex v10 = vertices[Index(i + 1, j)];
                    Vertex v01 = vertices[Index(i, j + 1)];
                    Vertex v11 = vertices[Index(i + 1, j + 1)];
                    triangles[t++] = new Triangle(v00, v10, v11);
                    triangles[t++] = new Triangle(v00, v11, v01);
                }
            }

            return new Mesh(vertices, triangles);

            int Index(int i, int j) => i * pointsPerSide + j;
        }
    }
}
