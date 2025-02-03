using UnityEngine;
using Photon.Pun;

public class CarController_2 : MonoBehaviour
{
    [SerializeField] private WheelCollider frontLeftCollider, frontRightCollider, backLeftCollider, backRightCollider;
    [SerializeField] private Transform frontLeftWheelTransform, frontRightWheelTransform, backLeftWheelTransform, backRightWheelTransform;
    [SerializeField] private Transform centerMass;
    [Header("Engine stuff")]
    [SerializeField] private float maxMotorTorque = 1500f;
    [SerializeField] private float maxWheelSteeringAngle = 35f;
    [SerializeField] private float brakeTorque = 3000f;

    private Rigidbody carRigidbody;
    private float verticalInput;
    private float horizontalInput;
    private float breakInput;

    private float speed;
    [SerializeField] private AnimationCurve steeringCurve;

    private bool isGameOver = false;
    private PhotonView playerView;

    private void Start()
    {
        playerView = GetComponent<PhotonView>();
        carRigidbody = GetComponent<Rigidbody>();
        carRigidbody.centerOfMass = centerMass.position;
    }
    private void Update()
    {
        if (playerView.IsMine && isGameOver == false)
        {
            Move();
            Brake();
            Steering();
            CheckInput();
        }
    }

    private void Move()
    {
        speed = carRigidbody.velocity.magnitude;

        frontLeftCollider.motorTorque = verticalInput * maxMotorTorque;
        frontRightCollider.motorTorque = verticalInput * maxMotorTorque;
        //backLeftCollider.motorTorque = verticalInput * maxMotorTorque;
        //backRightCollider.motorTorque = verticalInput * maxMotorTorque;
        UpdateWheelPosition(frontLeftCollider, frontLeftWheelTransform);
        UpdateWheelPosition(frontRightCollider, frontRightWheelTransform);
        UpdateWheelPosition(backLeftCollider, backLeftWheelTransform);
        UpdateWheelPosition(backRightCollider, backRightWheelTransform);
    }
    private void Steering()
    {
        float steeringAngle = horizontalInput * steeringCurve.Evaluate(speed);
        float slipAngle=Vector3.Angle(transform.forward, carRigidbody.velocity-transform.forward);
        if (slipAngle < 120) steeringAngle += Vector3.SignedAngle(transform.forward, carRigidbody.velocity, Vector3.up);
        steeringAngle = Mathf.Clamp(steeringAngle, -48, 48);

        frontLeftCollider.steerAngle = steeringAngle;
        frontRightCollider.steerAngle = steeringAngle;
    }
    private void Brake()
    {
        frontLeftCollider.brakeTorque = breakInput * brakeTorque * 0.7f;
        frontRightCollider.brakeTorque = breakInput * brakeTorque * 0.7f;
        backLeftCollider.brakeTorque = breakInput * brakeTorque * 0.3f;
        backRightCollider.brakeTorque = breakInput * brakeTorque * 0.3f;
    }
    private void CheckInput()
    {
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");

        float movingDiraction=Vector3.Dot(transform.forward, carRigidbody.velocity);
        if (movingDiraction < -0.5f && verticalInput > 0 || movingDiraction > 0.5f && verticalInput < 0)
        {
            breakInput = Mathf.Abs(verticalInput);
        }
        else breakInput = 0;
    }
    private void UpdateWheelPosition(WheelCollider collider, Transform wheel)
    {
        Vector3 position;
        Quaternion quaternion;
        collider.GetWorldPose(out position, out quaternion);
        wheel.position = position;
        wheel.rotation = quaternion;
    }
    private void GameOver()
    {
        isGameOver = true;
        frontLeftCollider.brakeTorque = brakeTorque;
        frontRightCollider.brakeTorque = brakeTorque;
        backLeftCollider.brakeTorque = brakeTorque;
        backRightCollider.brakeTorque = brakeTorque;
    }
}