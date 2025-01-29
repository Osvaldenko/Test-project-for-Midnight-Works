using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviourPunCallbacks
{
    [SerializeField] private CarData carData;
    [SerializeField] private List<GameObject> playerPrefab;
    [SerializeField] private GameObject cameraPrefab;

    private void Start()
    {
        if (PhotonNetwork.IsConnectedAndReady)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(40f, 55f), 1f, Random.Range(110f, 125f));
            GameObject playerCar = PhotonNetwork.Instantiate(playerPrefab[ChooseCarToSpawn()].name, spawnPosition, Quaternion.identity);

            GameObject playerCamera = PhotonNetwork.Instantiate(cameraPrefab.name, spawnPosition + Vector3.up, Quaternion.identity);
            playerCamera.GetComponent<CameraController>().SetCarToFollow(playerCar.transform);
        }
    }

    private int ChooseCarToSpawn()
    {
        int selectedCarID = 0;
        switch (PlayerPrefs.GetString("SelectedCarName"))
        {
            case "Regular":
                selectedCarID = 0;
                break;
            case "PickUpTruck":
                selectedCarID = 1;
                break;
            case "Hammer":
                selectedCarID = 2;
                break;
            case "Taxi":
                selectedCarID = 3;
                break;
        }
        return selectedCarID;
    }
}