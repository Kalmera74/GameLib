using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Facebook.Unity;
using Firebase.Analytics;
using GameAnalyticsSDK;
using Unity.Services.Analytics;
using Unity.Services.Core;
using Unity.Services.Core.Environments;
using UnityEngine;
using UnityEngine.Analytics;

namespace Mobiversite
{
    public class AnalyticInitializer : MonoBehaviour
    {
        void Awake()
        {
            DontDestroyOnLoad(this);

            InitFirebase();
            InitFacebook();
            InitGameAnalytics();

        }


        async void Start()
        {
            await InitUnityAnalyticAsync();
        }



        private void InitGameAnalytics()
        {
            GameAnalytics.Initialize();
            GameAnalyticsILRD.SubscribeMaxImpressions();

        }



        private static void InitFirebase()
        {
            Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
            {
                var dependencyStatus = task.Result;
                if (dependencyStatus == Firebase.DependencyStatus.Available)
                {
                    FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
                }
                else
                {
                    UnityEngine.Debug.LogError(System.String.Format(
                      "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                }
            });
        }

        private void InitFacebook()
        {
            if (!FB.IsInitialized)
            {
                FB.Init(() => FB.ActivateApp());
            }
            else
            {
                FB.ActivateApp();
            }
        }


        private async Task InitUnityAnalyticAsync()
        {
            try
            {
                var options = new InitializationOptions();
                Analytics.initializeOnStartup = true;

                options.SetEnvironmentName("production");
                await UnityServices.InitializeAsync(options);


            }
            catch (ConsentCheckException e)
            {
                // Something went wrong when checking the GeoIP, check the e.Reason and handle appropriately.
            }
        }

    }
}
