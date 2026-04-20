using System;
using UnityEngine;

public class CursorState : MonoBehaviour
{
    private static CursorState _instance;

    public static CursorState Instance
    {
        get
        {
            return _instance;
        }
    }
    private bool isLocked = true;

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        Lock();
    }

    private void Update()
    {
        if (!WindowState.IsWindowOpen)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
                    Lock();
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Unlock();
            }
        }
    }

    public void Lock()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        isLocked = true;
        CameraController.Instance.Lock();
    }
    
    public void Unlock()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        isLocked = false;
        CameraController.Instance.Unlock();
    }
    
}