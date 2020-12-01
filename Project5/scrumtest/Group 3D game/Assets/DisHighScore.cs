using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisHighScore : MonoBehaviour
{
    public Text winText;
    public GameObject Score;
    public string highscore;
    void Start()
    {
        winText = GameObject.Find("Win").GetComponent<Text>();
        Score = GameObject.Find("Score");
    }
    private void Update()
    {
        winText.text = "GAME OVER\nHigh Score: " + Score.GetComponent<HighScore>().DisplayHighscore() + "\n Level1 = "+ Score.GetComponent<HighScore>().Lv1Score + "\n Level2 = " + Score.GetComponent<HighScore>().Lv2Score + "\n Level3 = " + Score.GetComponent<HighScore>().Lv3Score;
    }

}
