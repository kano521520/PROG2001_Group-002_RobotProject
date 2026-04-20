
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;
    [HideInInspector]public Transform camTransform;
    public float walkSpeed = 5f;
    public float runSpeed = 8f;

    private float horizontal;
    private float vertical;
    private bool isRunning;
    public bool canMove = true;

    private void Start()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody>();
        if (camTransform == null)
            camTransform = Camera.main.transform;
    }

    void Update()
    {
        if (!canMove || WindowState.IsWindowOpen)
            return;
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        isRunning = Input.GetKey(KeyCode.LeftShift);
    }
    public void LockMove()
    {
        canMove = false;
        horizontal = 0;
        vertical = 0;
        rb.velocity = new Vector3(0, rb.velocity.y, 0);
    }
    public void UnlockMove()
    {
        canMove = true;
    }

    void FixedUpdate()
    {
        if (!canMove || WindowState.IsWindowOpen)
            return;
        
        float currentSpeed = isRunning ? runSpeed : walkSpeed;
        Vector3 moveDir = transform.forward * vertical + transform.right * horizontal;
        Vector3 lookDir = camTransform.forward;
        lookDir.y = 0;
        if (lookDir != Vector3.zero)
        {
            transform.forward = lookDir;
        }
        
        rb.velocity = new Vector3(moveDir.x * currentSpeed, rb.velocity.y, moveDir.z * currentSpeed);
    }
}