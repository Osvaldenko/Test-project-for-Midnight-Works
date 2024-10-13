using Photon.Pun;
using Photon.Realtime;
using System;
using UnityEngine;

public class PhotonLauncher : MonoBehaviourPunCallbacks
{
    public static event Action OnWaitingForPlayers;

    [SerializeField] private int maxPlayers = 2;

    public override void OnConnectedToMaster()
    {
        CreateRoom();
    }
    public override void OnJoinedRoom()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == maxPlayers)
        {
            StartGame();
        }
        else
        {
            OnWaitingForPlayers?.Invoke();
        }
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == maxPlayers)
        {
            StartGame();
        }
        else
        {
            OnWaitingForPlayers?.Invoke();
        }
    }
    private void CreateRoom()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = (byte)maxPlayers;
        PhotonNetwork.JoinOrCreateRoom("Room1", roomOptions, TypedLobby.Default);
    }
    private void StartGame()
    {
        PhotonNetwork.LoadLevel("GameplayLevelScene");
    }
    public void StartSoloGame()
    {
        maxPlayers = 1;
        PhotonNetwork.ConnectUsingSettings();
    }
    public void StartMultiplayerGame()
    {
        PhotonNetwork.ConnectUsingSettings();
    }
}