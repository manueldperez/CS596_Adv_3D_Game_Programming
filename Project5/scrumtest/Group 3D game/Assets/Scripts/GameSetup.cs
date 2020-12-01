using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSetup : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameSetup GS;
    public Transform[] spawnPoints;
    public int spawnpickerz =0; 
    private void OnEnable()
    {
        if(GameSetup.GS == null)
        {
            GameSetup.GS = this;
        }
    }
    public int spawn()
    {
        return spawnpickerz++;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
