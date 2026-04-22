using UnityEngine;

public class ChildWithMeshGenerator : MonoBehaviour
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
        MeshGenerator meshGenerator = new MeshGenerator(spriteRenderer.sprite, subdivisions);
        meshFilter.mesh = meshGenerator.CreateSubdividedMesh();
        meshRenderer.material = new Material(spriteRenderer.material);
    }

    private void DisableSpriteRenderer()
    {
        spriteRenderer.enabled = false;
    }
}
