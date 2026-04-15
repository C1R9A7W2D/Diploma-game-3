using UnityEngine;

public class ShaderEffectApplier : MonoBehaviour
{
    [SerializeField] private Material shaderMaterial;
    [SerializeField] private Texture2D spriteTexture;

    void Start()
    {
        // Добавляем MeshRenderer, если его нет
        MeshRenderer renderer = GetComponent<MeshRenderer>();
        if (renderer == null)
            renderer = gameObject.AddComponent<MeshRenderer>();

        // Создаём новый материал на основе шейдера
        if (shaderMaterial != null)
        {
            Material matInstance = new Material(shaderMaterial);
            matInstance.SetTexture("_MainTex", spriteTexture);
            renderer.material = matInstance;
        }
        else
        {
            Debug.LogWarning("Материал не назначен!");
        }
    }
}
