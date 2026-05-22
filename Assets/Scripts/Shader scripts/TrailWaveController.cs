using UnityEngine;

public class TrailWaveController : MonoBehaviour
{
    [SerializeField]
    private bool isHorizontal = false;

    private Material trailMaterial;
    private float wavePhase = 0f;

    void Start()
    {
        TrailRenderer trail = GetComponent<TrailRenderer>();
        trailMaterial = trail.material;
    }

    void Update()
    {
        wavePhase = Time.time;
        trailMaterial.SetFloat("_WavePhase", wavePhase);
        trailMaterial.SetFloat("_IsHorizontal", isHorizontal ? 1f : 0f);
    }
}
