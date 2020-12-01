using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    public static PlayerSpawn PS;

    public Transform[] spawnPos;

    public int spawnPlacement = 0;

    private void OnEnable()
    {
        if (PS == null)
        {
            PS = this;
        }
    }
    public int spawn()
    {
        return spawnPlacement++;
    }
}