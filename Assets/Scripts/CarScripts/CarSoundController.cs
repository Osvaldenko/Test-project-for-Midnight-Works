using UnityEngine;

public class CarSoundController : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [Header("Audio Sources")]
    [SerializeField] private AudioSource engineStart;
    [SerializeField] private AudioSource idleSound;
    [SerializeField] private AudioSource driveSound;
    [SerializeField] private AudioSource driftSound;

    [Header("Settings")]
    [SerializeField] private float minPitch = 0.8f;
    [SerializeField] private float maxPitch = 2.0f;
    [SerializeField] private float pitchSpeedFactor = 0.02f;

    private bool engineStarted = false;

    private void Update()
    {
        float speed = _rigidbody.velocity.magnitude;
        AdjustDriveSoundPitch(speed);

        if (speed > 1f && driveSound.isPlaying == false)
        {
            idleSound.Stop();
            driveSound.loop = true;
            driveSound.Play();
        }
        else if (speed <= 1f && idleSound.isPlaying == false && engineStarted)
        {
            driveSound.Stop();
            idleSound.Play();
        }
    }

    public void PlayDriftSound()
    {
        if (driftSound.isPlaying == false)
        {
            driftSound.Play();
        }
    }
    public void StopDriftSound()
    {
        if (driftSound.isPlaying)
        {
            driftSound.Stop();
        }
    }
    public void StartEngine()
    {
        if (engineStarted == false)
        {
            engineStarted = true;
            engineStart.Play();
            Invoke(nameof(PlayIdleSound), engineStart.clip.length);
        }
    }

    private void PlayIdleSound()
    {
        idleSound.loop = true;
        idleSound.Play();
    }
    private void AdjustDriveSoundPitch(float speed)
    {
        float pitch = Mathf.Lerp(minPitch, maxPitch, speed * pitchSpeedFactor);
        driveSound.pitch = pitch;
    }
}