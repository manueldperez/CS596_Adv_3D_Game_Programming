﻿using UnityEngine;

public class CurrentRoomCanvas : MonoBehaviour
{
    [SerializeField]
    private PlayerListingsMenu _playerListingsMenu;

    [SerializeField]
    private LeaveRoomMenu _leaveRoomMenu;

    public LeaveRoomMenu LeaveRoomMenu { get { return _leaveRoomMenu; } }

    private RoomCanvases _roomCanvases;

    public void FirstInitialize(RoomCanvases canvases)
    {
        _roomCanvases = canvases;
        _playerListingsMenu.FirstInitialize(canvases);
        _leaveRoomMenu.FirstInitialize(canvases);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

}