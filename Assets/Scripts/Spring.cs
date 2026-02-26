using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Spring : MonoBehaviour, IScoreGiver
{
    public struct ColParams
    {
        public Rigidbody2D rb;
        public Collider2D col;
        public Vector2 pos;
        public float radius;

        public (Vector2 dir, float centerDist) GetDirAndDist(ColParams otherParams)
        {
            // Вектор от нашего объекта к другому
            Vector2 dir = (otherParams.pos - pos).normalized;

            // Текущее расстояние между центрами
            float centerDist = Vector2.Distance(pos, otherParams.pos);

            return (dir, centerDist);
        }

        public float GetPenetration(ColParams otherParams, float centerDist)
        {
            return Mathf.Max(0f, (radius + otherParams.radius) - centerDist + epsilon);
        }
    }

    [Header("Настройки упругости")]
    [Tooltip("Коэффициент упругости (чем больше, тем сильнее отталкивание)")]
    public float springConstant = 30f;

    [Tooltip("Максимальная сила, чтобы избежать слишком сильных рывков")]
    public float maxForce = 150f;

    private const float epsilon = 0.01f;
    private Vector2 dir;
    private float centerDist;

    private ColParams myParams;

    private void Start()
    {
        Collider2D collider = GetComponent<Collider2D>();

        myParams = new ColParams
        {
            rb = null,
            col = collider,
            pos = collider.bounds.center,
            radius = collider.bounds.extents.magnitude
        };
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ApplyRepulsion(collision);
        GiveScore();
    }
    private void OnCollisionStay2D(Collision2D collision) => ApplyRepulsion(collision);

    private void ApplyRepulsion(Collision2D collision)
    {
        ColParams otherParams = GetCollisionParameters(collision);

        if (otherParams.rb == null) return;

        (dir, centerDist) = myParams.GetDirAndDist(otherParams);

        // Глубина проникновения (положительная, если объекты перекрываются)
        float penetration = GetPenetration(otherParams);
        if (penetration <= 0f) return; // нет перекрытия – ничего не делаем

        Vector2 force = GetRepulsionForce(penetration);

        otherParams.rb.AddForce(force, ForceMode2D.Impulse);
    }

    private ColParams GetCollisionParameters(Collision2D collision)
    {
        Collider2D collider = collision.collider;

        ColParams otherParams = new ColParams
        {
            rb = collision.rigidbody,
            col = collider,
            pos = collider.bounds.center,
            radius = collider.bounds.extents.magnitude
        };

        return otherParams;
    }

    private float GetPenetration(ColParams otherParams)
    {
        return myParams.GetPenetration(otherParams, centerDist);
    }

    private Vector2 GetRepulsionForce(float penetration)
    {
        float forceMag = Mathf.Clamp(springConstant * penetration, 0f, maxForce);
        Vector2 force = dir * forceMag;
        return force;
    }

    public void GiveScore()
    {
        PlayerScore.UpdateScore(10);
    }
}
