using Unity.Mathematics;
using UnityEngine;

public class InteractiveLightWave : ShaderEffectOnCollision
{
    public float waveSpeed = 2f;

    protected override void SetCharacteristics(Vector2 hitPosition)
    {
        base.SetCharacteristics(hitPosition);
        materialProxy.SetFloat("_WaveSpeed", waveSpeed);
    }
}
