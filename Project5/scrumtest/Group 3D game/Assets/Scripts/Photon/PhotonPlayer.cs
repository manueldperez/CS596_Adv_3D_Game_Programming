using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PhotonPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    private PhotonView PV;
    public GameObject myAvatar;
    public GameObject cam;
    public List<String> prefabList = new List<String>();
    void Start()
    {
        PV = GetComponent<PhotonView>();
        int spawnPicker = UnityEngine.Random.Range(0, GameSetup.GS.spawnPoints.Length);
        int prefabIndex = UnityEngine.Random.Range(0, prefabList.Count - 1);
        if (PV.IsMine)
        {
            myAvatar = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", prefabList[prefabIndex]), GameSetup.GS.spawnPoints[spawnPicker].position,
                GameSetup.GS.spawnPoints[spawnPicker].rotation, 0);
            cam = Instantiate(cam, GameSetup.GS.spawnPoints[spawnPicker].position,
                GameSetup.GS.spawnPoints[spawnPicker].rotation);
        }
        cam.gameObject.AddComponent<CameraControllers>();
        cam.transform.rotation = Quaternion.Euler(50f, -90f, 0);
        cam.gameObject.GetComponent<CameraControllers>().player = myAvatar;
        cam.gameObject.GetComponent<CameraControllers>().offset = new Vector3(150f, 250f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
