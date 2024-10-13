using System;
using UnityEngine;
using Photon.Pun;

public class CarController : MonoBehaviour
{
    public static event Action OnCarDrifting;
    public static event Action OnCarStopedDrifting;

    [SerializeField] private CarSoundController carSoundController;
    [SerializeField] Rigidbody carRigidbody;
    [SerializeField] private WheelCollider frontLeftCollider, frontRightCollider, backLeftCollider, backRightCollider;
    [SerializeField] private Transform frontLeftWheelTransform, frontRightWheelTransform, backLeftWheelTransform, backRightWheelTransform;
    [Header("Engine stuff")]
    [SerializeField] private float maxMotorTorque = 1500f;
    [SerializeField] private float maxWheelSteeringAngle = 30f;
    [SerializeField] private float brakeTorque = 3000f;

    [Header("Drifting stuff")]
    [SerializeField] private float driftSpeedThreshold = 50f;
    [SerializeField] private float driftTimeWindow = 0.3f;
    [SerializeField] private float driftFactor = 1.5f;
    [SerializeField] private GameObject trailPrefab;

    private TrailRenderer[] tireTrails;
    private bool isGameOver = false;
    private bool isDrifting = false;
    private float lastSpacePressTime = 0;
    private WheelFrictionCurve originalSideWaysFriction;
    private WheelFrictionCurve driftSideWaysFriction;
    private bool isBraking;
    private PhotonView playerView;

    private void OnEnable()
    {
        TimerUIController.onGameOver += GameOver;
    }
    private void OnDisable()
    {
        TimerUIController.onGameOver -= GameOver;
    }
    private void Start()
    {
        carSoundController.StartEngine();

        tireTrails = new TrailRenderer[2];
        SetTrailsToBackWheel(0, backLeftWheelTransform);
        SetTrailsToBackWheel(1, backRightWheelTransform);

        originalSideWaysFriction = backLeftCollider.sidewaysFriction;
        driftSideWaysFriction = backLeftCollider.sidewaysFriction;
        driftSideWaysFriction.stiffness = originalSideWaysFriction.stiffness / driftFactor;
        playerView = GetComponent<PhotonView>();
    }
    private void Update()
    {
        if (playerView.IsMine && isGameOver == false)
        {
            float enginePower = maxMotorTorque * Input.GetAxis("Vertical");
            float steeringPower = maxWheelSteeringAngle * Input.GetAxis("Horizontal");

            float carSpeed = carRigidbody.velocity.magnitude * 3.6f;//перевел метры в секунду в километры в час
                                                                    
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isBraking = true;
                if (isBraking && carSpeed > driftSpeedThreshold)
                {
                    if (Time.time - lastSpacePressTime < driftTimeWindow && !isDrifting)
                    {
                        StartDrift();
                    }
                    lastSpacePressTime = Time.time;
                }
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                isBraking = false;
            }
            float brakePower = isBraking ? brakeTorque : 0f;
            if (isDrifting && carSpeed > driftSpeedThreshold) OnCarDrifting?.Invoke();
            if (!isBraking && isDrifting)
            {
                StopDrift();
            }

            frontLeftCollider.steerAngle = steeringPower;
            frontRightCollider.steerAngle = steeringPower;
            backLeftCollider.motorTorque = enginePower;
            backRightCollider.motorTorque = enginePower;

            if (!isDrifting)
            {
                frontLeftCollider.brakeTorque = brakePower;
                frontRightCollider.brakeTorque = brakePower;
                backLeftCollider.brakeTorque = brakePower;
                backRightCollider.brakeTorque = brakePower;
            }
            else
            {
                frontLeftCollider.brakeTorque = 10000;
                frontRightCollider.brakeTorque = 10000;
            }
            foreach (var trail in tireTrails)
            {
                trail.emitting = isDrifting;
            }
            if (isDrifting)
            {
                carSoundController.PlayDriftSound();
            }
            else
            {
                carSoundController.StopDriftSound();
            }
            UpdateWheelPosition(frontLeftCollider, frontLeftWheelTransform);
            UpdateWheelPosition(frontRightCollider, frontRightWheelTransform);
            UpdateWheelPosition(backLeftCollider, backLeftWheelTransform);
            UpdateWheelPosition(backRightCollider, backRightWheelTransform);
        }
    }

    private void SetTrailsToBackWheel(int i, Transform wheel)
    {
        GameObject trail = Instantiate(trailPrefab, wheel);
        trail.transform.localPosition = Vector3.zero;
        tireTrails[i] = trail.GetComponent<TrailRenderer>();
        tireTrails[i].emitting = false;
    }
    private void GameOver()
    {
        isGameOver = true;
        frontLeftCollider.brakeTorque = brakeTorque;
        frontRightCollider.brakeTorque = brakeTorque;
        backLeftCollider.brakeTorque = brakeTorque;
        backRightCollider.brakeTorque = brakeTorque;
    }
    private void StopDrift()
    {
        isDrifting = false;
        backLeftCollider.sidewaysFriction = originalSideWaysFriction;
        backRightCollider.sidewaysFriction = originalSideWaysFriction;
        OnCarStopedDrifting?.Invoke();
        Debug.Log("Дрифт деактивирован.");
    }
    private void StartDrift()
    {
        isDrifting = true;
        backLeftCollider.sidewaysFriction = driftSideWaysFriction;
        backRightCollider.sidewaysFriction = driftSideWaysFriction;
        Debug.Log("Дрифт активирован!");
    }
    private void UpdateWheelPosition(WheelCollider collider, Transform wheel)
    {
        Vector3 position;
        Quaternion quaternion;
        collider.GetWorldPose(out position, out quaternion);
        wheel.position = position;
        wheel.rotation = quaternion;
    }
}