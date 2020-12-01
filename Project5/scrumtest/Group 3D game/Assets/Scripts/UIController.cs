using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public void Single()
    {
        SceneManager.LoadScene("Single");

        //Time.timeScale = 0f;
        //Debug.Log("Hello");
    }

    public void Multiplayer()
    {
        SceneManager.LoadScene("Multiplayer");
        //Time.timeScale = 0f;
    }
    public void MultiplayerLobby()
    {
        SceneManager.LoadScene("MultiplayerLobby");
        //Time.timeScale = 0f;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

 

}