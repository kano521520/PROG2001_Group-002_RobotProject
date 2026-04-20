using UnityEngine;
using UnityEngine.UI; 

public class NewBehaviourScript : MonoBehaviour
{
    [Header("Settings")]
    public float moveSpeed = 15f;
    public float jumpForce = 5f;

    [Header("UI References")]
    public GameObject winUI;    
    public Text counterText;    

    private Rigidbody rb;
    private Transform cam;
    private bool isGrounded;
    private int trashTotal = 5; 
    private int collectedCount = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cam = Camera.main.transform;

        UpdateCounterUI();
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
            Collider col = other.GetComponent<Collider>();
            if (col != null && col.enabled)
            {
                col.enabled = false;
                Destroy(other.gameObject);

                collectedCount++;
                UpdateCounterUI(); 

                if (collectedCount >= trashTotal && winUI != null)
                {
                    winUI.SetActive(true);
                    if (counterText != null) counterText.gameObject.SetActive(false);
                }
            }
        }
    }

    void UpdateCounterUI()
    {
        if (counterText != null)
        {
            counterText.text = "TRASH COLLECTED: " + collectedCount + " / " + trashTotal;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor")) isGrounded = true;
    }
}