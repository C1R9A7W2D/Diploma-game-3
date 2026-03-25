using UnityEngine;

public class FlickingLight : MonoBehaviour
{
    public float flashSpeed = 2f;
    public float timeOffset = 0;

    void Start()
    {
        var spriteRenderer = GetComponent<SpriteRenderer>();
        PropertyBlockProxy propertyBlock = new PropertyBlockProxy(spriteRenderer);
        propertyBlock.SetFloat("_TimeOffset", timeOffset);
    }
}
