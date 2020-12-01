using Photon.Pun;
using System.IO;
using UnityEngine;

public class PunPlaya : MonoBehaviour
{
    private PhotonView PV;

    public GameObject myAvatar;

    void Start()
    {
        PV = GetComponent<PhotonView>();

        int randSpawn = Random.Range(0, PlayerSpawn.PS.spawnPos.Length);

        if (PV.IsMine)
        {
            myAvatar = PhotonNetwork.Instantiate(Path.Combine("Prefabs", "Player"),
                        PlayerSpawn.PS.spawnPos[randSpawn].position,
                        PlayerSpawn.PS.spawnPos[randSpawn].rotation, 0);
        }
    }
}
