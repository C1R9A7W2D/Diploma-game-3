using UnityEngine;

public class FallTrigger : MonoBehaviour
{
    readonly Vector2 TELEPORT_POSITION = new Vector2(-0.7f, 5);

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
        Ball ball = collision.GetComponent<Ball>();
        ball.Teleport(TELEPORT_POSITION);
    }
}
