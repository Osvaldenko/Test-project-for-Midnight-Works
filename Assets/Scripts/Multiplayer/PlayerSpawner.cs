using Photon.Pun;
using UnityEngine;

public class PlayerSpawner : MonoBehaviourPunCallbacks
{
    [SerializeField] private CarData carData;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject cameraPrefab;

    private void Start()
    {
        if (PhotonNetwork.IsConnectedAndReady)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(40f, 55f), 1f, Random.Range(110f, 125f));
            GameObject playerCar = PhotonNetwork.Instantiate(playerPrefab.name, spawnPosition, Quaternion.identity);

            GameObject playerCamera = PhotonNetwork.Instantiate(cameraPrefab.name, spawnPosition + Vector3.up, Quaternion.identity);
            playerCamera.GetComponent<CameraController>().SetCarToFollow(playerCar.transform);
        }
    }
}