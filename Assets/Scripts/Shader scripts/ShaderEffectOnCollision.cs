using System;
using System.Linq;
using UnityEngine;

public class ShaderEffectOnCollision : MonoBehaviour
{
    protected IActivatedMaterial materialProxy;

    [Tooltip("Общий материал. Добавлять, только если эффект должен распространяться на все объекты с этим шейдером сразу.")]
    [SerializeField]
    protected float duration = 5f;
    [SerializeField]
    private Material material;

    protected Collision2D lastCollision;
    protected float timer = 0f;

    private bool isActive = false;

    private void Start()
    {
        SetMaterialProxy();
        SetInitialCharacteristics();
    }

    private void SetMaterialProxy()
    {
        if (SeparateEffects())
            materialProxy = new PropertyBlockProxy(GetComponent<SpriteRenderer>());
        else
            materialProxy = new MaterialProxy(material);
    }

    protected virtual void SetInitialCharacteristics()
    {
        materialProxy.SetFloat("_Duration", duration);
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
            UpdateCharacteristics();

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

    protected virtual void UpdateCharacteristics()
    {
        
    }

    private void DeactivateShader()
    {
        lastCollision = null;
        isActive = false;
        timer = 0f;
        materialProxy.SetLocalTime(0f);
        materialProxy.SetActive(0f);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        lastCollision = collision;
        Vector2 hitPosition = GetHitPosition(collision);
        ActivateShader(hitPosition);
    }

    private static Vector2 GetHitPosition(Collision2D collision)
    {
        return collision.GetContact(0).point;
    }

    public void OnMouseDown()
    {
        Vector2 clickPosition = GetMouseWorldPosition();
        ActivateShader(clickPosition);
    }

    private static Vector2 GetMouseWorldPosition()
    {
        Vector3 mouseScreenPos = Input.mousePosition;
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
        return (Vector2)mouseWorldPos;
    }

    private void ActivateShader(Vector2 contactPoint)
    {
        MarkFlags();
        SetCharacteristics(contactPoint);
    }

    private void MarkFlags()
    {
        isActive = true;
        timer = 0f;
    }

    virtual protected void SetCharacteristics(Vector2 contactPoint)
    {
        materialProxy.SetVector("_ContactPoint", contactPoint);
        materialProxy.SetLocalTime(0f);
        materialProxy.SetActive(1f);
    }

    private void OnApplicationQuit()
    {
        DeactivateShader();
    }
}
