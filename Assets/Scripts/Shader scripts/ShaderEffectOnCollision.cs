using System;
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

    protected virtual void Start()
    {
        SetMaterialProxy();
        SetInitialCharacteristics();
    }

    protected virtual void SetMaterialProxy()
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

    protected virtual void SetInitialCharacteristics()
    {
        materialProxy.SetFloat("_Duration", duration);
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
        isActive = false;
        timer = 0f;
        materialProxy.SetLocalTime(0f);
        materialProxy.SetActive(0f);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        ActivateShader(collision);
    }

    private static Vector2 GetHitPosition(Collision2D collision)
    {
        return collision.GetContact(0).point;
    }

    private void ActivateShader(Collision2D collision)
    {
        MarkFlags();
        SetCharacteristics(collision);
    }

    private void MarkFlags()
    {
        isActive = true;
        timer = 0f;
    }

    virtual protected void SetCharacteristics(Collision2D collision)
    {
        Vector2 hitPosition = GetHitPosition(collision);

        materialProxy.SetVector("_ContactPoint", hitPosition);
        materialProxy.SetLocalTime(0f);
        materialProxy.SetActive(1f);
    }

    private void OnApplicationQuit()
    {
        DeactivateShader();
    }
}
