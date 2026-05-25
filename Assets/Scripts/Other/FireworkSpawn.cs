using UnityEngine;

public class FireworkSpawn : MonoBehaviour
{
    [SerializeField]
    GameObject firework;

    void Update()
    {
        if (IsLMBPressed())
            SpawnFirework();
    }

    private bool IsLMBPressed()
    {
        return Input.GetMouseButtonUp(0);
    }

    private void SpawnFirework()
    {
        Instantiate(firework, MouseMethods.GetMouseWorldPosition(), Quaternion.identity);
    }
}
