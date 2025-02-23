using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PinkDiamondManager : MonoBehaviour
{
    private int pinkDiamonds = 0;
    public TMP_Text pinkDiamondsText;
    public TMP_Text message;
    private void OnEnable()
  {
    IronSourceEvents.onSdkInitializationCompletedEvent += SdkInitialized;
    IronSourceRewardedVideoEvents.onAdOpenedEvent += RewardedVideoOnAdOpenedEvent;
    IronSourceRewardedVideoEvents.onAdClosedEvent += RewardedVideoOnAdClosedEvent;
    IronSourceRewardedVideoEvents.onAdAvailableEvent += RewardedVideoOnAdAvailable;
    IronSourceRewardedVideoEvents.onAdUnavailableEvent += RewardedVideoOnAdUnavailable;
    IronSourceRewardedVideoEvents.onAdShowFailedEvent += RewardedVideoOnAdShowFailedEvent;
    IronSourceRewardedVideoEvents.onAdRewardedEvent += RewardedVideoOnAdRewardedEvent;
    IronSourceRewardedVideoEvents.onAdClickedEvent += RewardedVideoOnAdClickedEvent;
  }
   void SdkInitialized()
    {
        Debug.Log("Sdk initialized");
    }
    void OnApplicationPause(bool isPaused)
    {
        IronSource.Agent.onApplicationPause(!isPaused);
    }
    #region Rewarded

    public void LoadRewarded() {
        IronSource.Agent.loadRewardedVideo();
    }

    public void ShowRewarded() {
        if (IronSource.Agent.isRewardedVideoAvailable())
        {
            IronSource.Agent.showRewardedVideo();   
        }
        else
        {
            message.text="Ad is not ready";
            Invoke("removeText",3f);    
        }
        LoadRewarded();
    }

    void RewardedVideoOnAdAvailable(IronSourceAdInfo adInfo)
    {
    }

    void RewardedVideoOnAdUnavailable()
    {
    }

    void RewardedVideoOnAdOpenedEvent(IronSourceAdInfo adInfo)
    {
        
    }

    void RewardedVideoOnAdClosedEvent(IronSourceAdInfo adInfo)
    {
       
    }

    void RewardedVideoOnAdRewardedEvent(IronSourcePlacement placement, IronSourceAdInfo adInfo)
    {
        pinkDiamonds+=1;
        PlayerPrefs.SetInt("PinkDiamonds",pinkDiamonds);
    }

    void RewardedVideoOnAdShowFailedEvent(IronSourceError error, IronSourceAdInfo adInfo)
    {
    }

    void RewardedVideoOnAdClickedEvent(IronSourcePlacement placement, IronSourceAdInfo adInfo)
    {
        
    }
    private void UpdatePinkDiamonds()
    {
        pinkDiamondsText.text = PlayerPrefs.GetInt("PinkDiamonds").ToString();
    }
    #endregion
    void Start()
    {
        LoadRewarded();
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        IronSource.Agent.init ("1e557f2cd");
        IronSource.Agent.validateIntegration();
        UpdatePinkDiamonds();
    }
    private void removeText()
    {
        message.text = "";
    }
    void Update()
    {
        UpdatePinkDiamonds();
    }

}
