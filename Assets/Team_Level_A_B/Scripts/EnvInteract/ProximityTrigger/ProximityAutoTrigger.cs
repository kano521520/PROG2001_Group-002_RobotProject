using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SphereCollider))]
public class ProximityAutoTrigger : MonoBehaviour
{
    public string triggerTag = "Player";
    public bool triggerOnce = false;
    public bool destroyAfterTrigger = false;
    public UnityEvent triggerEvent;

    private static ProximityAutoTrigger activeInstance;
    private bool hasTriggeredInSession = false;
    private bool totalTriggered = false;
    private Collider cld;

    private void Start()
    {
        cld = GetComponent<Collider>();
        if (cld != null) cld.isTrigger = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (triggerOnce && totalTriggered) return;

        if (activeInstance == null && !hasTriggeredInSession && other.CompareTag(triggerTag))
        {
            activeInstance = this;
            hasTriggeredInSession = true;
            totalTriggered = true;

            triggerEvent?.Invoke();

            if (destroyAfterTrigger)
            {
                activeInstance = null;
                Destroy(gameObject);
            }
            else
            {
                activeInstance = null; 
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(triggerTag))
        {
            hasTriggeredInSession = false;
            if (activeInstance == this) activeInstance = null;
        }
    }
}