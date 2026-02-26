using UnityEngine;

public class Flipper : MonoBehaviour
{
    [SerializeField] private bool isLeft;

    private Vector3 pivot;
    private float minAngle;
    private float maxAngle;
    private const float rotationSpeed = 500f;
    private bool wantUp;

    void Start()
    {
        GetPivotPoint();
        GetMinAndMaxAngles();
    }

    private void GetPivotPoint()
    {
        pivot = transform.GetChild(0).position;
    }

    private void GetMinAndMaxAngles()
    {
        // начальный угол
        float startZ = transform.eulerAngles.z;
        if (isLeft)
        {
            minAngle = startZ;
            maxAngle = minAngle + 45f;
        }
        else
        {
            maxAngle = startZ;
            minAngle = maxAngle - 45f;
        }
    }

    void Update()
    {
        wantUp = Input.GetKey(KeyCode.Space);
    }

    void FixedUpdate()
    {
        float step = rotationSpeed * Time.fixedDeltaTime;

        if (wantUp)
        {
            if (isLeft) 
                RotateCounterClockwise(step);
            else 
                RotateClockwise(step);
        }
        else
        {
            if (isLeft) 
                RotateClockwise(step);
            else 
                RotateCounterClockwise(step);
        }
    }

    private void RotateCounterClockwise(float step)
    {
        float angle = transform.eulerAngles.z;
        if (angle < maxAngle)
            transform.RotateAround(pivot, Vector3.forward, step);
    }

    private void RotateClockwise(float step)
    {
        float angle = transform.eulerAngles.z;
        if (angle > minAngle)
            transform.RotateAround(pivot, Vector3.forward, -step);
    }
}
