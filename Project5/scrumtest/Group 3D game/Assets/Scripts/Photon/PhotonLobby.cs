using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonLobby : MonoBehaviourPunCallbacks
{
    public static PhotonLobby lobby;
    public GameObject connectButton;
    public GameObject cancelButton; 
    private void Awake()
    {
        lobby = this;
    }
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();//connects master photon server
        
    }
    public override void OnConnectedToMaster()
    {
        Debug.Log("Player has connected");
        PhotonNetwork.AutomaticallySyncScene = true; 
        connectButton.SetActive(true);
    }
    public void OnConnectClick()
    {
        Debug.Log("Connect button clicked");
        connectButton.SetActive(false);
        cancelButton.SetActive(true);
        PhotonNetwork.JoinRandomRoom();
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Tried to join room but failed");
        createRoom();
    }
    void createRoom()
    {
        Debug.Log("room was created");
        int randomRoomName = Random.Range(0, 10000);
        RoomOptions roomOps = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = 4 };
        PhotonNetwork.CreateRoom("Room" + randomRoomName, roomOps);
    }
  
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("failed create room");
        createRoom();
    }

    public void OnCancleButtonClicked()
    {
        Debug.Log("cacnle button ");
        cancelButton.SetActive(false);
        connectButton.SetActive(true);
        PhotonNetwork.LeaveRoom();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
