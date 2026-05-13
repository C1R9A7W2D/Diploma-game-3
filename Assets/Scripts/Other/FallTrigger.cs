using UnityEngine;

public class FallTrigger : MonoBehaviour
{
    private Vector2 teleportPosition = new Vector2(-0.7f, 5);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsBall(collision))
        {
            Teleport(collision);
        }
    }

    private static bool IsBall(Collider2D collision)
    {
        return collision.CompareTag("Ball");
    }

    private void Teleport(Collider2D collision)
    {
        teleportPosition.x = Random.Range(-2, 2);
        Ball ball = collision.GetComponent<Ball>();
        ball.Teleport(teleportPosition);
    }
}
