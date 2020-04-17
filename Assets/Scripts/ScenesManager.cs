using UnityEngine.SceneManagement;
using UnityEngine;

public class ScenesManager : MonoBehaviour
{
    public void startGame()
    {
        SceneManager.LoadScene("Gameplay");
    }

    public void closeApp()
    {
        Application.Quit();
    }
}
