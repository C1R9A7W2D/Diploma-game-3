using UnityEngine;

public class SpriteMeshHybrid : MonoBehaviour
{
    [SerializeField] private int subdivisions = 10;
    private GameObject meshChild;
    public MeshRenderer deformationMeshRenderer { get; private set; }

    void Start()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null || spriteRenderer.sprite == null)
        {
            Debug.LogError("SpriteRenderer не найден!");
            return;
        }

        // Создаём ОТДЕЛЬНЫЙ GameObject с мешем
        meshChild = new GameObject("DeformationMesh");
        meshChild.transform.SetParent(transform);
        meshChild.transform.localPosition = Vector3.zero;
        meshChild.transform.localScale = Vector3.one;

        // Добавляем компоненты на дочерний объект
        MeshFilter meshFilter = meshChild.AddComponent<MeshFilter>();
        deformationMeshRenderer = meshChild.AddComponent<MeshRenderer>();

        // Создаём и применяем меш
        Mesh deformedMesh = CreateSubdividedMesh(spriteRenderer.sprite, subdivisions);
        meshFilter.mesh = deformedMesh;

        // Копируем материал из SpriteRenderer
        Material matCopy = new Material(spriteRenderer.material);
        deformationMeshRenderer.material = matCopy;

        // Отключаем исходный SpriteRenderer, но не удаляем его
        spriteRenderer.enabled = false;

        Debug.Log("Гибридная система создана");
    }

    Mesh CreateSubdividedMesh(Sprite sprite, int subdivisions)
    {
        Mesh mesh = new Mesh();
        mesh.name = "SubdividedSpriteMesh";

        Bounds spriteBounds = sprite.bounds;
        float width = spriteBounds.size.x;
        float height = spriteBounds.size.y;

        Vector3[] vertices = new Vector3[(subdivisions + 1) * (subdivisions + 1)];
        Vector2[] uv = new Vector2[(subdivisions + 1) * (subdivisions + 1)];

        Rect spriteRect = sprite.rect;
        Texture2D spriteTexture = sprite.texture;

        for (int y = 0; y <= subdivisions; y++)
        {
            for (int x = 0; x <= subdivisions; x++)
            {
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
        }

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

                triangles[triIndex++] = a;
                triangles[triIndex++] = c;
                triangles[triIndex++] = b;
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

        return mesh;
    }
}
