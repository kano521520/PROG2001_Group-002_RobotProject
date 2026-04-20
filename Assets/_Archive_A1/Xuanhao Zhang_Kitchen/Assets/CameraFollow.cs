using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;            
    public float smoothSpeed = 0.125f;  

    [Header("Initial Perspective")]
    public Vector3 offset = new Vector3(0, 8, -10);

    [Header("Rotation Settings")]
    public float angleStep = 45f;     

    void LateUpdate()
    {
        if (target != null)
        {
            Vector3 desiredPosition = target.position + offset;

            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;

            transform.LookAt(target.position + Vector3.up * 1f);
        }
    }

    public void RotateRight()
    {
        offset = Quaternion.Euler(0, angleStep, 0) * offset;
    }

    public void RotateLeft()
    {
        offset = Quaternion.Euler(0, -angleStep, 0) * offset;
    }
}
