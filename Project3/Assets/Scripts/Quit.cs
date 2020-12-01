using UnityEngine;
using UnityEngine.SceneManagement;

public class Quit : MonoBehaviour
{
    public void OnClick_Quit()
    {
        SceneManager.LoadScene(0);
    }

    public void OnClick_QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
    Debug.Log("Quit the game");
    Application.Quit();

#endif

    }
}
