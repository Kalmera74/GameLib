using System;
using System.Collections;
using System.Collections.Generic;
using Mobiversite.AnalyticsHelper;
using Mobiversite.GameLib.DevLib.Core;
using UnityEngine;

namespace Mobiversite
{
    public class AdsManager : MonoBehaviour
    {
        [SerializeField] private string AndroidAdUnitID;
        [SerializeField] private string IOSAdUnitID;
        [SerializeField] private string SDKKey = "xR50v4uM6wKVdihtDoJALoJp868ATbR7BtiADMEG-w0TEkTPeUsboGEzpx5TBUtSgTEz5c4Zpv62d12tfbrnkL";
        private string _adUnitId = "c06c57c02d51504d";
        private int _retryAttempt;
        public static AdsManager Instance;
        public event Action OnAdClosed;
        private bool failed;

        void Awake()
        {
            Instance = this;

            if (!PlayerPrefs.HasKey("showads"))
            {
                PlayerPrefs.SetInt("showads", 1);
            }


            _adUnitId = AndroidAdUnitID;
#if UNITY_IOS
            _adUnitId = IOSAdUnitID;
#endif


            InitializeInterstitialAds();
            DontDestroyOnLoad(this);
        }
        public void InitializeInterstitialAds()
        {
            MaxSdkCallbacks.OnSdkInitializedEvent += sdkConfiguration =>
            {
                MaxSdkCallbacks.Interstitial.OnAdLoadedEvent += OnInterstitialLoadedEvent;
                MaxSdkCallbacks.Interstitial.OnAdLoadFailedEvent += OnInterstitialLoadFailedEvent;
                MaxSdkCallbacks.Interstitial.OnAdDisplayedEvent += OnInterstitialDisplayedEvent;
                MaxSdkCallbacks.Interstitial.OnAdClickedEvent += OnInterstitialClickedEvent;
                MaxSdkCallbacks.Interstitial.OnAdHiddenEvent += OnInterstitialHiddenEvent;
                MaxSdkCallbacks.Interstitial.OnAdDisplayFailedEvent += OnInterstitialAdFailedToDisplayEvent;
                MaxSdk.LoadInterstitial(_adUnitId);

            };

            MaxSdk.SetSdkKey(SDKKey);

            MaxSdk.InitializeSdk();


        }




        private void OnInterstitialLoadedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            // Interstitial ad is ready for you to show. MaxSdk.IsInterstitialReady(adUnitId) now returns 'true'

            // Reset retry attempt
            Debug.LogWarning($"!!!  Loaded The Ads");
            failed = false;
            AnalyticsLogger.LogInterstitialLoaded(adInfo.NetworkName, _adUnitId);
        }

        private void OnInterstitialLoadFailedEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo)
        {
            // Interstitial ad failed to load 
            // AppLovin recommends that you retry with exponentially higher delays, up to a maximum delay (in this case 64 seconds)
            Debug.LogWarning($"!!! Failed To Load The Ads");
            failed = true;

        }

        private void OnInterstitialDisplayedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            Debug.LogWarning($"!!! Displayed The Ads");
            AnalyticsLogger.LogInterstitialDisplayed(adInfo.NetworkName, _adUnitId);

        }

        private void OnInterstitialAdFailedToDisplayEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo, MaxSdkBase.AdInfo adInfo)
        {
            // Interstitial ad failed to display. AppLovin recommends that you load the next ad.
            AnalyticsLogger.LogInterstitialDisplayFailed(adInfo.NetworkName, _adUnitId);
            OnAdClosed?.Invoke();
            Debug.LogWarning($"!!! Failed To Display The Ads");
            LoadInterstitial();
        }

        private void OnInterstitialClickedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            OnAdClosed?.Invoke();
            AnalyticsLogger.LogInterstitialClicked(adInfo.NetworkName, _adUnitId);
        }

        private void OnInterstitialHiddenEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            // Interstitial ad is hidden. Pre-load the next ad.
            OnAdClosed?.Invoke();
            LoadInterstitial();

        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ShowInterstitial();
            }
        }
        public void ShowInterstitial()
        {
            Debug.LogWarning($"!!! Called To Show The Ads");
            bool canShowAds = PlayerPrefs.GetInt("showads") == 1;
            if (!canShowAds || failed)
            {
                Debug.LogWarning($"!!! Invoke Close First If");
                OnAdClosed?.Invoke();
                return;
            }
            if (MaxSdk.IsInterstitialReady(_adUnitId))
            {
                int currentLevel = SceneManager.Instance.GetCurrentNavigableLevelNumber();
                if (currentLevel < 3 || currentLevel == 4 || currentLevel == 6)
                {
                    Debug.LogWarning($"!!! Invoke Close Second If");
                    OnAdClosed?.Invoke();
                    return;
                }

                Debug.LogWarning($"!!! Calling Show Ads");
                MaxSdk.ShowInterstitial(_adUnitId);
                Debug.LogWarning($"!!! Called Show Ads");

            }
            else
            {
                Debug.LogWarning($"!!! Ads not Ready Skip It");
                OnAdClosed?.Invoke();
            }
        }
        private void LoadInterstitial()
        {
            MaxSdk.LoadInterstitial(_adUnitId);

        }


    }
}
