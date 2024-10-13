using UnityEngine;

public class IronSourceController : MonoBehaviour
{
    [SerializeField] private string androidAppKey = "1fd44c77d";
    [SerializeField] private string iosAppKey;

#if UNITY_ANDROID
    private void OnEnable()
    {
        IronSourceEvents.onRewardedVideoAdRewardedEvent += OnRewardedVideoAdRewarded;
    }

    private void OnDisable()
    {
        IronSourceEvents.onRewardedVideoAdRewardedEvent -= OnRewardedVideoAdRewarded;
    }
#endif
    private void Start()
    {
#if UNITY_ANDROID
        string appKey = androidAppKey;
#elif UNITY_IOS
        string appKey = iosAppKey;
#else
        string appKey = "";
#endif

        IronSource.Agent.init(appKey);
    }

    private void OnRewardedVideoAdRewarded(IronSourcePlacement placement)
    {
        
    }

    public void ShowRewardedAd()
    {
        if (IronSource.Agent.isRewardedVideoAvailable())
        {
            IronSource.Agent.showRewardedVideo();
        }
        else
        {
            Debug.Log("Rewarded video is not available yet.");
        }
    }
}