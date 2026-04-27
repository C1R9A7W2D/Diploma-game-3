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

    protected override void SetCharacteristics(Vector2 contactPoint)
    {
        base.SetCharacteristics(contactPoint);

        // Интенсивность натяжения зависит от скорости объекта
        Rigidbody2D rb = lastCollision.rigidbody;

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
