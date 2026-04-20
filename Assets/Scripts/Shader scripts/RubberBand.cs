using Unity.Mathematics;
using UnityEngine;

public class RubberBand : ShaderEffectOnCollision
{
    [SerializeField]
    private float stretchAmount = 0f;
    [SerializeField]
    private float maxStretch = 1.5f;
    [SerializeField]
    private float stretchRadius = 0.3f;
    [SerializeField]
    private float recoverySpeed = 3f;

    

    protected override void SetMaterialProxy()
    {
        // Получаем MeshRenderer от дочернего объекта через SpriteMeshHybrid
        SpriteMeshHybrid hybrid = GetComponent<SpriteMeshHybrid>();

        if (hybrid != null && hybrid.deformationMeshRenderer != null)
        {
            materialProxy = new PropertyBlockProxy(hybrid.deformationMeshRenderer);
        }
        else
        {
            Debug.LogError("SpriteMeshHybrid не найден или его MeshRenderer не инициализирован!");
        }
    }

    protected override void SetInitialCharacteristics()
    {
        materialProxy.SetFloat("_StretchRadius", stretchRadius);
    }

    protected override void UpdateCharacteristics()
    {
        // Плавное восстановление формы
        stretchAmount = Mathf.Lerp(stretchAmount, 0f, Time.deltaTime * recoverySpeed);
        materialProxy.SetFloat("_StretchAmount", stretchAmount);
    }

    protected override void SetCharacteristics(Collision2D collision)
    {
        base.SetCharacteristics(collision);

        // Интенсивность натяжения зависит от скорости объекта
        Rigidbody2D rb = collision.rigidbody;

        if (rb != null)
        {
            stretchAmount = Mathf.Clamp(rb.linearVelocity.magnitude * 0.5f, 0, maxStretch);
        }
        else
        {
            stretchAmount = maxStretch * 0.5f;
        }
    }
}
