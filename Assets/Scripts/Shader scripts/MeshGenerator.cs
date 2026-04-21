using UnityEngine;

public class MeshGenerator : MonoBehaviour
{
    public MeshRenderer meshRenderer { get; private set; }

    [SerializeField] 
    private int subdivisions = 10;

    private SpriteRenderer spriteRenderer;
    private GameObject meshChild;
    private MeshFilter meshFilter;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (NoSprite())
        {
            Debug.LogError("SpriteRenderer не найден!");
            return;
        }

        CreateChildObject();
        CreateMeshAndMaterial();
        DisableSpriteRenderer();
    }

    private bool NoSprite()
    {
        return spriteRenderer == null || spriteRenderer.sprite == null;
    }

    private void CreateChildObject()
    {
        meshChild = new GameObject("DeformationMesh");
        SetPositionAndScale();
        CreateMeshFilterAndRenderer();
    }

    private void SetPositionAndScale()
    {
        meshChild.transform.SetParent(transform);
        meshChild.transform.localPosition = Vector3.zero;
        meshChild.transform.localScale = Vector3.one;
    }

    private void CreateMeshFilterAndRenderer()
    {
        meshFilter = meshChild.AddComponent<MeshFilter>();
        meshRenderer = meshChild.AddComponent<MeshRenderer>();
    }

    private void CreateMeshAndMaterial()
    {
        meshFilter.mesh = CreateSubdividedMesh(spriteRenderer.sprite);
        meshRenderer.material = new Material(spriteRenderer.material);
    }

    private void DisableSpriteRenderer()
    {
        spriteRenderer.enabled = false;
    }

    Mesh CreateSubdividedMesh(Sprite sprite)
    {
        Mesh mesh = new Mesh();
        mesh.name = "SubdividedSpriteMesh";
        Vector3[] vertices;
        Vector2[] uv;
        CalculateVerticesAndUV(sprite, out vertices, out uv);

        int[] triangles = TriangulateDivisions();

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        return mesh;
    }

    private void CalculateVerticesAndUV(Sprite sprite, out Vector3[] vertices, out Vector2[] uv)
    {
        Bounds spriteBounds = sprite.bounds;
        float width = spriteBounds.size.x;
        float height = spriteBounds.size.y;

        vertices = new Vector3[(subdivisions + 1) * (subdivisions + 1)];
        uv = new Vector2[(subdivisions + 1) * (subdivisions + 1)];

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
    }

    private int[] TriangulateDivisions()
    {
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

        return triangles;
    }
}
