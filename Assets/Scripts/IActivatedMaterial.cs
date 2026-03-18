using UnityEngine;

public interface IActivatedMaterial
{
    void SetActive(float amount);
    void SetLocalTime(float amount);
    public void SetFloat(string propertyName, float value);
    void SetVector(string propertyName, Vector4 value);
}

public class MaterialProxy : IActivatedMaterial
{
    Material material;

    public MaterialProxy(Material material)
    {
        this.material = material;
    }

    public void SetActive(float amount)
    {
        material.SetFloat("_IsActive", amount);
    }

    public void SetLocalTime(float amount)
    {
        material.SetFloat("_LocalTime", amount);
    }

    public void SetFloat(string propertyName, float value)
    {
        material.SetFloat(propertyName, value);
    }

    public void SetVector(string propertyName, Vector4 value)
    {
        material.SetVector(propertyName, value);
    }
}

public class PropertyBlockProxy : IActivatedMaterial
{
    SpriteRenderer spriteRenderer;
    MaterialPropertyBlock propertyBlock;

    public PropertyBlockProxy(SpriteRenderer spriteRenderer)
    {
        this.spriteRenderer = spriteRenderer;
        propertyBlock = new MaterialPropertyBlock();
        spriteRenderer.SetPropertyBlock(propertyBlock);
    }

    public void SetActive(float amount)
    {
        propertyBlock.SetFloat("_IsActive", amount);
        spriteRenderer.SetPropertyBlock(propertyBlock);
    }

    public void SetLocalTime(float amount)
    {
        propertyBlock.SetFloat("_LocalTime", amount);
        spriteRenderer.SetPropertyBlock(propertyBlock);
    }

    public void SetFloat(string propertyName, float value)
    {
        propertyBlock.SetFloat(propertyName, value);
        spriteRenderer.SetPropertyBlock(propertyBlock);
    }

    public void SetVector(string propertyName, Vector4 value)
    {
        propertyBlock.SetVector(propertyName, value);
        spriteRenderer.SetPropertyBlock(propertyBlock);
    }
}
