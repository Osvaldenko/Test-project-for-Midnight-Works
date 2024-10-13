using Photon.Pun;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform carToFollow;
    [SerializeField] private float followSpeed;
    [SerializeField] private float lookChangeSpeed;
    [SerializeField] private Vector3 offSet;

    private PhotonView playerView;

    private void Start()
    {
        playerView = GetComponent<PhotonView>();
        if (playerView.IsMine == false)
        {
            this.gameObject.SetActive(false);
        }
    }
    private void FixedUpdate()
    {
        if (playerView.IsMine)
        {
            LookAtCar();
            FollowCar();
        }
    }

    private void FollowCar()
    {
        Vector3 targetPosition = carToFollow.position + (carToFollow.forward * offSet.z + carToFollow.right * offSet.x + carToFollow.up * offSet.y);
        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
    }
    private void LookAtCar()
    {
        Vector3 lookDirection = carToFollow.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(lookDirection, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, lookChangeSpeed * Time.deltaTime);
    }
    public void SetCarToFollow(Transform carTransform)
    {
        carToFollow = carTransform;
    }
}