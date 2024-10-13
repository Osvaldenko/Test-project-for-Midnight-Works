using System;
using System.Collections;
using UnityEngine;

public class IronSourcePCController : MonoBehaviour //эмулирую рекламу на ПК версии
{
    public static event Action OnAdCompletedEvent;
    public static event Action<float> OnAdStarted;

    [SerializeField] private bool isTesting = true;
    [SerializeField] private float adDuration = 5f;

    private bool adIsPlaying = false;

    public void ShowRewardedAd()
    {
#if UNITY_STANDALONE
        if (isTesting)
        {
            if (!adIsPlaying)
            {
                OnAdStarted?.Invoke(adDuration);
                StartCoroutine(SimulateRewardedAd());
            }
        }
#endif
    }

    private IEnumerator SimulateRewardedAd()
    {
        adIsPlaying = true;
        yield return new WaitForSeconds(adDuration);
        OnAdCompletedEvent?.Invoke();
        adIsPlaying = false;
    }
}