using UnityEngine;

public class FallTrigger : MonoBehaviour
{
    [SerializeField]
    private float positionFalloff = 0;
    private Vector2 baseTeleportPosition = new Vector2(1.3f, 5);
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
        Vector2 telePos = baseTeleportPosition;
        telePos.x += Random.Range(-positionFalloff, positionFalloff);
        ball.Teleport(telePos);
    }

    private void SetCollisionNull()
    {
        currentCollision = null;
    }
}
