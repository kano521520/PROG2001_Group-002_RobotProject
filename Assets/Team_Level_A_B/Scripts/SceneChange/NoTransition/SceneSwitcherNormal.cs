using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcherNormal : MonoBehaviour
{
    // Call SceneTransitionManager's SwitchScene method with given scene name
    public void SwitchToScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
}