using UnityEngine;
using UnityEngine.UI; 

public class RobotCollector : MonoBehaviour
{
    [Header("UI Settings")]
    public GameObject winUI;

    private int trashTotal;   
    private int collectedCount = 0; 

    void Start()
    {
        trashTotal = GameObject.FindGameObjectsWithTag("Trash").Length;

        if (winUI != null) winUI.SetActive(false);

        Debug.Log("Mission Start! Total trash: " + trashTotal);
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

                Debug.Log("Collected: " + collectedCount + " / " + trashTotal);

                if (collectedCount >= trashTotal && trashTotal > 0)
                {
                    ShowWinScreen();
                }
            }
        }
    }

    void ShowWinScreen()
    {
        if (winUI != null) winUI.SetActive(true);
        Debug.Log("Level Cleared! All trash collected.");
    }
}
