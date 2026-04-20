using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    [Header("Settings")]
    public float moveSpeed = 10f;
    public float jumpForce = 5f;
    public GameObject winUI;

    private Rigidbody rb;
    private Transform cam;
    private bool isGrounded;
    private int trashTotal;
    private int collectedCount = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cam = Camera.main.transform; 

        trashTotal = GameObject.FindGameObjectsWithTag("Trash").Length;
        if (winUI != null) winUI.SetActive(false);

        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 forward = cam.forward;
        Vector3 right = cam.right;
        forward.y = 0; 
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        Vector3 moveDir = (forward * v + right * h).normalized;

        if (moveDir.magnitude > 0.1f)
        {
            rb.velocity = new Vector3(moveDir.x * moveSpeed, rb.velocity.y, moveDir.z * moveSpeed);

            Quaternion targetRot = Quaternion.LookRotation(moveDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, targetRot.eulerAngles.y, 0), 0.2f);
        }
        else
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Trash"))
        {
            other.gameObject.SetActive(false);
            collectedCount++;
            if (collectedCount >= trashTotal && winUI != null) winUI.SetActive(true);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor")) isGrounded = true;
    }
}