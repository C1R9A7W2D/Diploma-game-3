using UnityEngine;

public class InteractiveLight : FlickingLight
{
    public float effectDuration = 2f; // Длительность эффекта в секундах

    private float timer = 0f;

    private bool isActive = false;
    protected new MaterialPropertyBlock propertyBlock;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        propertyBlock = new MaterialPropertyBlock();

        propertyBlock.SetFloat("_TimeOffset", timeOffset);
        spriteRenderer.SetPropertyBlock(propertyBlock);
    }

    void Update()
    {
        if (isActive)
        {
            timer += Time.deltaTime;

            if (timer >= effectDuration)
            {
                isActive = false;
                timer = 0f;
                propertyBlock.SetFloat("_IsActive", 0f);
                spriteRenderer.SetPropertyBlock(propertyBlock);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("collision occured");
        isActive = true;
        timer = 0f;

        propertyBlock.SetFloat("_IsActive", 1f);
        spriteRenderer.SetPropertyBlock(propertyBlock);
    }
}
