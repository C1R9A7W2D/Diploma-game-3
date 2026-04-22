using UnityEngine;

public class MeshGenerator
{
    private Sprite sprite;
    private int subdivisions;
    private Vector3[] vertices;
    private Vector2[] uv;
    private int[] triangles;

    public MeshGenerator(Sprite sprite, int subdivisions) 
    {
        this.sprite = sprite;
        this.subdivisions = subdivisions;

        vertices = new Vector3[(subdivisions + 1) * (subdivisions + 1)];
        uv = new Vector2[(subdivisions + 1) * (subdivisions + 1)];

        triangles = new int[subdivisions * subdivisions * 6];
    }

    public Mesh CreateSubdividedMesh()
    {
        Mesh mesh = new Mesh();
        mesh.name = "SubdividedSpriteMesh";

        CalculateVerticesAndUV();
        TriangulateDivisions();
        ApplyCharacteristicsTo(mesh);

        return mesh;
    }

    private void CalculateVerticesAndUV()
    {
        for (int y = 0; y <= subdivisions; y++)
        {
            CalculateVerticesAndUVForColumn(y);
        }
    }

    private void CalculateVerticesAndUVForColumn(int y)
    {
        for (int x = 0; x <= subdivisions; x++)
        {
            CalculateVerticeAndUVForPoint(x, y);
        }
    }

    private void CalculateVerticeAndUVForPoint(int x, int y)
    {
        Bounds spriteBounds = sprite.bounds;
        float width = spriteBounds.size.x;
        float height = spriteBounds.size.y;

        Rect spriteRect = sprite.rect;
        Texture2D spriteTexture = sprite.texture;

        float xNorm = x / (float)subdivisions;
        float yNorm = y / (float)subdivisions;

        int index = y * (subdivisions + 1) + x;
        vertices[index] = new Vector3(
            (xNorm - 0.5f) * width,
            (yNorm - 0.5f) * height,
            0
        );

        uv[index] = new Vector2(
            spriteRect.x / spriteTexture.width + xNorm * spriteRect.width / spriteTexture.width,
            spriteRect.y / spriteTexture.height + yNorm * spriteRect.height / spriteTexture.height
        );
    }

    private void TriangulateDivisions()
    {
        int triIndex = 0;

        for (int y = 0; y < subdivisions; y++)
        {
            triIndex = TriangulateForColumn(triIndex, y);
        }
    }

    private int TriangulateForColumn(int triIndex, int y)
    {
        for (int x = 0; x < subdivisions; x++)
        {
            triIndex = TriangulateForPoint(triIndex, x, y);
        }

        return triIndex;
    }

    private int TriangulateForPoint(int triIndex, int x, int y)
    {
        int a = y * (subdivisions + 1) + x;
        int b = a + 1;
        int c = a + (subdivisions + 1);
        int d = c + 1;

        triangles[triIndex++] = a;
        triangles[triIndex++] = c;
        triangles[triIndex++] = b;
        triangles[triIndex++] = b;
        triangles[triIndex++] = c;
        triangles[triIndex++] = d;
        return triIndex;
    }

    private void ApplyCharacteristicsTo(Mesh mesh)
    {
        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
    }
}
