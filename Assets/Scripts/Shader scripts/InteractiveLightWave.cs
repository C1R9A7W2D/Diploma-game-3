using Unity.Mathematics;
using UnityEngine;

public class InteractiveLightWave : ShaderEffectOnCollision
{
    public float waveSpeed = 2f;

    protected override void SetCharacteristics(Collision2D collision)
    {
        base.SetCharacteristics(collision);
        materialProxy.SetFloat("_WaveSpeed", waveSpeed);
    }
}
