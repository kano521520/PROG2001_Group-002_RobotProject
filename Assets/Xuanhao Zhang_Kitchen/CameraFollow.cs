using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.125f;
    public float sensitivity = 3f;

    [Header("Distance Settings")]
    public float distance = 12f;
    public float heightOffset = 1.5f;

    [Header("Angle Limits")]
    public float minPitch = -10f;
    public float maxPitch = 60f;

    private float currentYaw = 0f;
    private float currentPitch = 20f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Vector3 angles = transform.eulerAngles;
        currentYaw = angles.y;
    }

    void LateUpdate()
    {
        if (target != null)
        {
            currentYaw += Input.GetAxis("Mouse X") * sensitivity;
            currentPitch -= Input.GetAxis("Mouse Y") * sensitivity;
            currentPitch = Mathf.Clamp(currentPitch, minPitch, maxPitch);

            Quaternion rotation = Quaternion.Euler(currentPitch, currentYaw, 0);
            Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
            Vector3 position = rotation * negDistance + target.position + Vector3.up * heightOffset;

            transform.rotation = rotation;
            transform.position = Vector3.Lerp(transform.position, position, smoothSpeed);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}