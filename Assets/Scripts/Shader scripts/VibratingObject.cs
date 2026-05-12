using UnityEngine;

public class VibratingObject : ShaderEffectOnCollision
{
    [SerializeField]
    private float frequency = 10f;
    [SerializeField]
    private float amplitude = 0.05f;
    private float intensity = 0f;

    protected override void UpdateCharacteristics()
    {
        intensity = (duration - timer) / duration;
        materialProxy.SetFloat("_VibrationIntensity", intensity);

        materialProxy.SetFloat("_VibrationFrequency", frequency);
        materialProxy.SetFloat("_VibrationAmplitude", amplitude);
    }
}
