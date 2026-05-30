using UnityEngine;

public class FallTrigger : MonoBehaviour
{
    private Vector2 teleportPosition = new Vector2(1.3f, 5);
    private Collider2D currentCollision;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SetCollision(collision);
        if (IsBall())
            Teleport();
        SetCollisionNull();
    }

    private void SetCollision(Collider2D collision)
    {
        currentCollision = collision;
    }

    private bool IsBall()
    {
        return currentCollision.CompareTag("Ball");
    }

    private void Teleport()
    {
        Ball ball = currentCollision.GetComponent<Ball>();
        ball.Teleport(teleportPosition);
    }

    private void SetCollisionNull()
    {
        currentCollision = null;
    }
}
