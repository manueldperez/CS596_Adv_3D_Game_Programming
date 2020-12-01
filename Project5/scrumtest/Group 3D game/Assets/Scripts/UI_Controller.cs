using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Controller : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("Level 1");

        //Time.timeScale = 0f;
        //Debug.Log("Hello");
    }

    public void Instructions()
    {
        SceneManager.LoadScene("Instruction");
        //Time.timeScale = 0f;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //Time.timeScale = 1f;
    }

    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
    Debug.Log("Quit the game");
    Application.Quit();

#endif
    }
}