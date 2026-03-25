using UnityEngine;

public class Ball : MonoBehaviour
{
    new Rigidbody2D rigidbody;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Teleport(Vector2 newPosition)
    {
        transform.position = newPosition;
        rigidbody.linearVelocity = Vector2.zero;
    }
}
