using UnityEngine;

public class Particle : MonoBehaviour
{
    float timer = 0;

    void Update()
    {
        if (timer > 10)
            Destroy(gameObject);
        timer += Time.deltaTime;
    }
}
