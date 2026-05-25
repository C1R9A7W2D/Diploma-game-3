using UnityEngine;

public class FireworkExplosion : MonoBehaviour
{
    [SerializeField]
    GameObject particle;

    private const int NUMBER_OF_PARTICLES = 10;
    private const int FORCE = 500;

    void Start()
    {
        for (int i = 0; i < NUMBER_OF_PARTICLES; i++)
        {
            int angle = 360 / NUMBER_OF_PARTICLES * i;
            SpawnParticle(angle);
        }
    }

    private void SpawnParticle(int angle)
    {
        GameObject instance = SpawnRotated(angle);
        LaunchForward(instance);
    }

    private GameObject SpawnRotated(int angle)
    {
        return Instantiate(particle, 
            transform.position, 
            Quaternion.AngleAxis(angle, Vector3.forward));
    }

    private void LaunchForward(GameObject instance)
    {
        var rb = instance.GetComponent<Rigidbody2D>();
        rb.AddRelativeForce(Vector2.up * FORCE);
    }
}
