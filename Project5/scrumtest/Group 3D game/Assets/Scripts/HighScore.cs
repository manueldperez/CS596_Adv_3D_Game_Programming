using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HighScore : MonoBehaviour
{
    public int Lv1Score;
    public int Lv2Score;
    public int Lv3Score;
    public int highscore;
    public void Start()
    {
        Lv1Score = 10000;
        Lv2Score = 10000;
        Lv3Score = 10000;
    }
    public void resetScore()
    {
        Lv1Score = 10000;
        Lv2Score = 10000;
        Lv3Score = 10000;
    }

    public void CompletedLevel1(int time)
    {
        Lv1Score -= Math.Min(time * 10, 9000);
    }
    public void CompletedLevel2(int time)
    {
        Lv2Score -= Math.Min(time * 10, 9000);
    }
    public void CompletedLevel3(int time)
    {
        Lv3Score -= Math.Min(time * 10, 9000);
    }

   
    public int DisplayHighscore()
    {

        return Lv1Score + Lv2Score + Lv3Score; 
    }

}
