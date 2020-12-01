﻿using Photon.Pun;
using UnityEngine;

public class LeaveRoomMenu : MonoBehaviour
{
    private RoomCanvases _roomCanvases;

    public void FirstInitialize(RoomCanvases canvases)
    {
        _roomCanvases = canvases;
    }

    public void OnClick_LeaveRoom()
    {
        PhotonNetwork.LeaveRoom(true);
        _roomCanvases.CurrentRoomCanvas.Hide();
    }
}