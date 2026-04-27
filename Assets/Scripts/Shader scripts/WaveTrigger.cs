using UnityEngine;

public class WaveTrigger : ShaderEffectOnCollision
{
    public float waveStrength = 0.3f;
    public float waveSpeed = 2f;

    protected override void SetCharacteristics(Vector2 contactPoint)
    {
        base.SetCharacteristics(contactPoint);
        materialProxy.SetFloat("_WaveStrength", waveStrength);
        materialProxy.SetFloat("_WaveSpeed", waveSpeed);
    }
}
