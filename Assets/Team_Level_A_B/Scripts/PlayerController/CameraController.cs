using System;
using UnityEngine;
using UnityEngine.EventSystems;



[RequireComponent(typeof(Camera))]
[RequireComponent(typeof(CursorState))]
public class CameraController : MonoBehaviour
{
    public static CameraController Instance
    {
        get
        {
            return _instance;
        }
    }

    private static CameraController _instance;
    public Transform target;
    public float mouseSensitivity = 2f;
    public Vector3 offset = new Vector3(0, 2, -5);
    public float minVerticalAngle = -45f;
    public float maxVerticalAngle = 45f;

    public float rotationX;
    public float rotationY;
    
    private bool isLocked = true;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
    }

    void Start()
    {
        Lock();
    }

    public void Lock()
    {
        isLocked = true;
    }
    
    public void Unlock()
    {
        isLocked = false;
    }

    void Update()
    {
        if (isLocked)
        {
            rotationX += Input.GetAxis("Mouse X") * mouseSensitivity;
            rotationY -= Input.GetAxis("Mouse Y") * mouseSensitivity;
            rotationY = Mathf.Clamp(rotationY, minVerticalAngle, maxVerticalAngle);
        }
    }

    void LateUpdate()
    {
        if (target == null) return;

        Quaternion rotation = Quaternion.Euler(rotationY, rotationX, 0);
        if (isLocked)
        {
            transform.rotation = rotation;
        }
        transform.position = target.position + rotation * offset;
    }
    
    
}

public static class WindowState
{
    public static bool IsWindowOpen;

    private static int _windowCount;

    public static void OpenSomeWindow()
    {
        IsWindowOpen = true;
        _windowCount++;
        CursorState.Instance.Unlock();
    }
    
    public static void CloseSomeWindow()
    {
        _windowCount--;
        if (_windowCount == 0)
        {
            IsWindowOpen = false;
            CursorState.Instance.Lock();
        }
    }
}