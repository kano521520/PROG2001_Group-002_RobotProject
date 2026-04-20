using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(SphereCollider))]
public class ProximityKeyTrigger : MonoBehaviour
{
    public string triggerTag = "Player";
    public string tipText = "[E] Interact";
    public KeyCode triggerKey = KeyCode.E;
    [Header("After trigger")]
    public bool DestroyThisComponent = false;
    public bool DestroyGameobject = false;
    public UnityEvent triggerEvent;

    private static ProximityKeyTrigger activeInstance;
    private bool hasTriggeredInSession = false;
    private GameObject tipCanvas;
    private Collider cld;

    private void Start()
    {
        cld = GetComponent<Collider>();
        if (cld != null) cld.isTrigger = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (activeInstance == null && !hasTriggeredInSession && other.CompareTag(triggerTag))
        {
            activeInstance = this;
            ShowTip();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(triggerTag))
        {
            hasTriggeredInSession = false;
            if (activeInstance == this)
            {
                ReleaseInstance();
            }
        }
    }

    private void Update()
    {
        if (activeInstance == this && Input.GetKeyDown(triggerKey))
        {
            triggerEvent?.Invoke();
            hasTriggeredInSession = true;
            
            bool killObj = DestroyGameobject;
            bool killComp = DestroyThisComponent;

            ReleaseInstance();

            if (killObj) Destroy(gameObject);
            else if (killComp) Destroy(this);
        }
    }

    private void ReleaseInstance()
    {
        if (tipCanvas != null) Destroy(tipCanvas);
        if (activeInstance == this) activeInstance = null;
    }

    private void ShowTip()
    {
        if (tipCanvas == null)
        {
            GameObject obj = Resources.Load<GameObject>("Prefabs/ProximityKeyTrigger");
            if (obj != null)
            {
                tipCanvas = Instantiate(obj);
                Text txt = tipCanvas.GetComponentInChildren<Text>();
                if (txt != null) txt.text = tipText;
            }
        }
    }
}