using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerJump : MonoBehaviour
{
    public Rigidbody rb;
    public Transform groundCheck;
    public float groundCheckDistance = 0.2f;
    public float jumpForce = 5f;
    public LayerMask groundLayer; // 建议增加层级检测

    private bool jumpRequested;

    private void Start()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            jumpRequested = true;
        }
    }

    void FixedUpdate()
    {
        if (jumpRequested)
        {
            // 使用 Impulse 瞬间施加力
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpRequested = false;
        }
    }

    public bool IsGrounded()
    {
        if (groundCheck == null) return false;
        return Physics.Raycast(groundCheck.position, Vector3.down, groundCheckDistance);
    }

    private void OnDrawGizmos()
    {
        if (groundCheck == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawRay(groundCheck.position, Vector3.down * groundCheckDistance);
    }
}