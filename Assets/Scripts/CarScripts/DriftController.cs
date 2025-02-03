using System;
using System.Collections;
using UnityEngine;

public class DriftController : MonoBehaviour
{
    public static event Action OnCarDrifting;
    public static event Action OnCarStoppedDrifting;

    [SerializeField] private WheelCollider backLeftCollider, backRightCollider;
    [SerializeField] private float driftSpeedThreshold = 50f;
    [SerializeField] private float driftTimeWindow = 0.3f;
    [SerializeField] private float driftFactor = 1.5f;
    [SerializeField] private GameObject trailPrefab;

    private TrailRenderer[] tireTrails;
    private bool isDrifting = false;
    private float lastSpacePressTime = 0;
    private WheelFrictionCurve originalSideWaysFriction;
    private WheelFrictionCurve driftSideWaysFriction;

    public bool IsDrifting { get { return isDrifting; } }

    private void Start()
    {
        tireTrails = new TrailRenderer[2];
        SetTrailsToBackWheel(0, backLeftCollider);
        SetTrailsToBackWheel(1, backRightCollider);

        originalSideWaysFriction = backLeftCollider.sidewaysFriction;
        driftSideWaysFriction = backLeftCollider.sidewaysFriction;
        driftSideWaysFriction.stiffness = originalSideWaysFriction.stiffness / driftFactor;
    }

    public void TryStartDrift(float carSpeed)
    {
        if (carSpeed > driftSpeedThreshold)
        {
            if (Time.time - lastSpacePressTime < driftTimeWindow && !isDrifting)
            {
                StartDrift();
            }
            lastSpacePressTime = Time.time;
        }
    }

    private void StartDrift()
    {
        isDrifting = true;
        backLeftCollider.sidewaysFriction = driftSideWaysFriction;
        backRightCollider.sidewaysFriction = driftSideWaysFriction;
        OnCarDrifting?.Invoke();

        foreach (var trail in tireTrails)
        {
            trail.emitting = true;
        }

        Debug.Log("Дрифт активирован!");
    }

    public void StopDrift()
    {
        if (!isDrifting) return;

        isDrifting = false;
        backLeftCollider.sidewaysFriction = originalSideWaysFriction;
        backRightCollider.sidewaysFriction = originalSideWaysFriction;
        OnCarStoppedDrifting?.Invoke();

        foreach (var trail in tireTrails)
        {
            trail.emitting = false;
        }

        Debug.Log("Дрифт деактивирован.");
    }

    private void SetTrailsToBackWheel(int i, WheelCollider wheel)
    {
        GameObject trail = Instantiate(trailPrefab, wheel.transform);
        trail.transform.localPosition = Vector3.zero;
        tireTrails[i] = trail.GetComponent<TrailRenderer>();
        tireTrails[i].emitting = false;
    }
}