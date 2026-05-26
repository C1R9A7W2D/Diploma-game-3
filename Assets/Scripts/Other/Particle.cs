using UnityEngine;

public class Particle : MonoBehaviour
{
    private const int FORCE = 500;
    private float timer = 0;
    private SpriteRenderer spriteRenderer;
    private TrailRenderer trailRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        trailRenderer = GetComponent<TrailRenderer>();
        LaunchForward();
    }

    private void LaunchForward()
    {
        var rb = GetComponent<Rigidbody2D>();
        rb.AddRelativeForce(Vector2.up * FORCE);
    }

    private void Update()
    {
        if (timer > 10)
            Destroy(gameObject);
        timer += Time.deltaTime;
    }

    public void SetColor(Color color)
    {
        MaterialPropertyBlock properties = new MaterialPropertyBlock();
        properties.SetColor("_Color", color);

        spriteRenderer.SetPropertyBlock(properties);
        trailRenderer.SetPropertyBlock(properties);
    }
}
