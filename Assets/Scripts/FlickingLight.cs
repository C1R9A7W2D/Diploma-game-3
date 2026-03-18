using UnityEngine;

public class FlickingLight : MonoBehaviour
{
    public float flashSpeed = 2f;
    public float timeOffset = 0;
    protected MaterialPropertyBlock propertyBlock;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        propertyBlock = new MaterialPropertyBlock();

        propertyBlock.SetFloat("_TimeOffset", timeOffset);
        spriteRenderer.SetPropertyBlock(propertyBlock);
    }
}
