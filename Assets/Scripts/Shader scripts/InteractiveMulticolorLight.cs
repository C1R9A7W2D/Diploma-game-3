using UnityEngine;

public class InteractiveMulticolorLight : MonoBehaviour
{
    [SerializeField]
    private ColorPalette[] availablePalettes;

    [SerializeField]
    private int objectIndex;

    private const int STATES_COUNT = 3;
    private SpriteRenderer spriteRenderer;
    private MaterialPropertyBlock propertyBlock;
    private int state;
    private bool blockKey;

    private void Start()
    {
        InitializeFields();
        SetColors();
        spriteRenderer.SetPropertyBlock(propertyBlock);
    }

    private void InitializeFields()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        propertyBlock = new MaterialPropertyBlock();
    }

    private void SetColors()
    {
        for (int i = 0; i < availablePalettes.Length; i++)
            SetColorFromPalette(i);
    }

    private void SetColorFromPalette(int i)
    {
        Color color = availablePalettes[i].colors[objectIndex];
        propertyBlock.SetColor($"_Color{i + 1}", color);
    }

    private void Update()
    {
        if (KeyPressed())
            SetNewColor();
        else if (KeyNotPressed())
            UnblockKey();
    }

    private bool KeyPressed()
    {
        return Input.GetKey(KeyCode.Return) && !blockKey;
    }

    private bool KeyNotPressed()
    {
        return !Input.GetKey(KeyCode.Return);
    }

    private void SetNewColor()
    {
        UpdateColor();
        BlockKey();
        SetStateInSprite();
    }

    private void UpdateColor()
    {
        if (state == STATES_COUNT - 1)
            state = 0;
        else
            state++;
    }

    private void BlockKey()
    {
        blockKey = true;
    }

    private void UnblockKey()
    {
        blockKey = false;
    }

    private void SetStateInSprite()
    {
        propertyBlock.SetFloat("_State", state);
        spriteRenderer.SetPropertyBlock(propertyBlock);
    }
}
