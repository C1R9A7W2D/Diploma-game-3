using UnityEngine;

public class InteractiveLightWave : ShaderEffectOnCollision
{
    public float waveSpeed = 2f;

    protected override void SetCharacteristics(Vector2 contactPoint)
    {
        base.SetCharacteristics(contactPoint);
        materialProxy.SetFloat("_WaveSpeed", waveSpeed);
    }
}