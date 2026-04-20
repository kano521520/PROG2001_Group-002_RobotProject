using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("KitchenLevel");
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Main_Menu");
    }
}