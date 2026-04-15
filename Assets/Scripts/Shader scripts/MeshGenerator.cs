using UnityEngine;

public class MeshGenerator : MonoBehaviour
{
    [SerializeField] private Texture2D spriteTexture;
    [SerializeField] private int subdivisions = 10;
    [SerializeField] private float pixelsPerUnit = 100f;

    void Start()
    {
        if (spriteTexture == null)
        {
            Debug.LogError("Текстура не назначена!");
            return;
        }

        Mesh generatedMesh = CreateGridMesh(spriteTexture, subdivisions);

        // Применяем меш к MeshFilter
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        if (meshFilter == null)
            meshFilter = gameObject.AddComponent<MeshFilter>();

        meshFilter.mesh = generatedMesh;

        // Добавляем коллайдер, если нужен
        if (GetComponent<MeshCollider>() == null)
            gameObject.AddComponent<MeshCollider>();
    }

    Mesh CreateGridMesh(Texture2D texture, int subdivisions)
    {
        Mesh mesh = new Mesh();
        mesh.name = "GeneratedGridMesh";

        float width = texture.width / pixelsPerUnit;
        float height = texture.height / pixelsPerUnit;

        int vertexCount = (subdivisions + 1) * (subdivisions + 1);
        Vector3[] vertices = new Vector3[vertexCount];
        Vector2[] uv = new Vector2[vertexCount];

        // Генерируем сетку вершин
        for (int y = 0; y <= subdivisions; y++)
        {
            for (int x = 0; x <= subdivisions; x++)
            {
                float xPos = (x / (float)subdivisions - 0.5f) * width;
                float yPos = (y / (float)subdivisions - 0.5f) * height;

                int index = y * (subdivisions + 1) + x;
                vertices[index] = new Vector3(xPos, yPos, 0);
                uv[index] = new Vector2(x / (float)subdivisions, y / (float)subdivisions);
            }
        }

        // Генерируем индексы треугольников
        int[] triangles = new int[subdivisions * subdivisions * 6];
        int triIndex = 0;

        for (int y = 0; y < subdivisions; y++)
        {
            for (int x = 0; x < subdivisions; x++)
            {
                int a = y * (subdivisions + 1) + x;
                int b = a + 1;
                int c = a + (subdivisions + 1);
                int d = c + 1;

                // Первый треугольник
                triangles[triIndex++] = a;
                triangles[triIndex++] = c;
                triangles[triIndex++] = b;

                // Второй треугольник
                triangles[triIndex++] = b;
                triangles[triIndex++] = c;
                triangles[triIndex++] = d;
            }
        }

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        Debug.Log($"Меш создан: {vertexCount} вершин, {subdivisions * subdivisions * 2} треугольников");

        return mesh;
    }
}
