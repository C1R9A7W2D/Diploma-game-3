using UnityEngine;

[CreateAssetMenu(fileName = "ColorPalette", menuName = "Game/Color Palette")]
public class ColorPalette : ScriptableObject
{
    public Color[] colors;
}

public class FireworkExplosion : MonoBehaviour
{
    [SerializeField]
    private GameObject particle;

    [SerializeField] 
    private ColorPalette[] availablePalettes;

    private const int NUMBER_OF_PARTICLES = 10;

    private Color[] palette;
    private float timer = 0;

    void Start()
    {
        GetRandomPalette();
        SpawnParticles();
    }

    private void GetRandomPalette()
    {
        int ind = Random.Range(0, availablePalettes.Length);
        palette = availablePalettes[ind].colors;
    }

    private void SpawnParticles()
    {
        for (int i = 0; i < NUMBER_OF_PARTICLES; i++)
        {
            SpawnWithIndex(i);
        }
    }

    private void SpawnWithIndex(int i)
    {
        int angle = 360 / NUMBER_OF_PARTICLES * i;
        Color color = palette[i % palette.Length];
        SpawnParticle(angle, color);
    }

    private void SpawnParticle(int angle, Color color)
    {
        GameObject instance = SpawnRotated(angle);
        Particle particleInstance = instance.GetComponent<Particle>();
        particleInstance.SetColor(color);
    }

    private GameObject SpawnRotated(int angle)
    {
        return Instantiate(particle, 
            transform.position, 
            Quaternion.AngleAxis(angle, Vector3.forward));
    }

    private void Update()
    {
        if (timer > 10)
            Destroy(gameObject);
        timer += Time.deltaTime;
    }
}
