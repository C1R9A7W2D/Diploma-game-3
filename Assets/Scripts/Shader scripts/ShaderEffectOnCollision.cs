using UnityEngine;

public class ShaderEffectOnCollision : MonoBehaviour
{
    protected IActivatedMaterial materialProxy;

    [Tooltip("Общий материал. Добавлять, только если эффект должен распространяться на все объекты с этим шейдером сразу.")]
    [SerializeField]
    private Material material;
    [SerializeField]
    private float duration = 5f;

    private float timer = 0f;
    private bool isActive = false;

    private void Start()
    {
        if (SeparateEffects())
            materialProxy = new PropertyBlockProxy(GetComponent<SpriteRenderer>());
        else
            materialProxy = new MaterialProxy(material);
    }

    private bool SeparateEffects()
    {
        return material == null;
    }

    void Update()
    {
        if (isActive)
        {
            UpdateTimer();

            if (timer >= duration)
            {
                DeactivateShader();
            }
        }
    }

    private void UpdateTimer()
    {
        timer += Time.deltaTime;
        materialProxy.SetLocalTime(timer);
    }

    private void DeactivateShader()
    {
        isActive = false;
        timer = 0f;
        materialProxy.SetLocalTime(0f);
        materialProxy.SetActive(0f);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 hitPosition = GetHitPosition(collision);
        ActivateShader(hitPosition);
    }

    private static Vector2 GetHitPosition(Collision2D collision)
    {
        return collision.GetContact(0).point;
    }

    private void ActivateShader(Vector2 hitPosition)
    {
        MarkFlags();
        SetCharacteristics(hitPosition);
    }

    private void MarkFlags()
    {
        isActive = true;
        timer = 0f;
    }

    virtual protected void SetCharacteristics(Vector2 hitPosition)
    {
        materialProxy.SetVector("_ContactPoint", hitPosition);
        materialProxy.SetLocalTime(0f);
        materialProxy.SetActive(1f);
    }

    private void OnApplicationQuit()
    {
        DeactivateShader();
    }
}
