using System.Collections;
using System.Collections.Generic;
using Firebase.Analytics;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Analytics;
using Unity.Services.Core.Environments;
using UnityEngine.Analytics;
using System;
using GameAnalyticsSDK;
namespace Mobiversite.AnalyticsHelper
{
    public static class AnalyticsLogger
    {

        public static void LogLevelLoadedEvent(int sceneIndex)
        {
            sceneIndex--;

            LogEvent("e_level_loaded", "p_level_load_scene_index", sceneIndex.ToString());
            GameAnalytics.NewDesignEvent("Loaded:Level_" + sceneIndex);
        }
        public static void LogLevelRestartEvent(int sceneIndex)
        {
            sceneIndex--;

            LogEvent("e_level_restarted", "p_level_restart_scene_index", sceneIndex.ToString());
            GameAnalytics.NewDesignEvent("Restarted:Level_" + sceneIndex);

        }
        public static void LogWinnedEvent(int sceneIndex)
        {
            sceneIndex--;

            LogEvent("e_winned", "p_level_win_scene_index", sceneIndex.ToString());
            GameAnalytics.NewDesignEvent("Winned:Level_" + sceneIndex);
        }

        public static void LogFailedEvent(int sceneIndex)
        {
            sceneIndex--;

            LogEvent("e_failed", "p_level_fail_scene_index", sceneIndex.ToString());
            GameAnalytics.NewDesignEvent("Failed:Level_" + sceneIndex);

        }
        public static void LogReplayedTheLevelEvent(int sceneIndex)
        {
            sceneIndex--;

            LogEvent("e_replayed", "p_level_replay_scene_index", sceneIndex.ToString());
            GameAnalytics.NewDesignEvent("Replayed:Level_" + sceneIndex);
        }
        public static void LogLevelPlayTime(int sceneIndex, float playTime)
        {
            sceneIndex--;


            string data = $"{sceneIndex}_{playTime}";

            GameAnalytics.NewDesignEvent("Level_" + sceneIndex + ":Time", playTime);


            FirebaseAnalytics.LogEvent("d_level_play_time", "p_index_time_concat", data);

            AnalyticsService.Instance.CustomData("e_level_play_time", new Dictionary<string, object>{
                {"p_level_play_time_scene_index",sceneIndex},
                  {"p_play_time",playTime}

            });

        }
        private static void LogEvent(string eventName, string parameterName, string value)
        {
            FirebaseAnalytics.LogEvent(eventName, parameterName, value);

            AnalyticsService.Instance.CustomData(eventName, new Dictionary<string, object>{
                {parameterName,value}
            });

        }
        private static void LogEvent(string eventName, string parameterName, int value)
        {
            FirebaseAnalytics.LogEvent(eventName, parameterName, value);



            AnalyticsService.Instance.CustomData(eventName, new Dictionary<string, object>{
                {parameterName,value}
            });

        }
        private static void LogEvent(string eventName, string parameterName, float value)
        {
            FirebaseAnalytics.LogEvent(eventName, parameterName, value);

            AnalyticsService.Instance.CustomData(eventName, new Dictionary<string, object>{
                {parameterName,value}
            });

        }

        internal static void LogInterstitialLoaded(string networkName, string adUnitId)
        {
            networkName = string.IsNullOrEmpty(networkName) ? "unknown" : networkName;

            GameAnalytics.NewAdEvent(GAAdAction.Loaded, GAAdType.Interstitial, networkName, adUnitId);
        }



        internal static void LogInterstitialDisplayed(string networkName, string adUnitId)
        {
            networkName = string.IsNullOrEmpty(networkName) ? "unknown" : networkName;

            GameAnalytics.NewAdEvent(GAAdAction.Show, GAAdType.Interstitial, networkName, adUnitId);
        }

        internal static void LogInterstitialDisplayFailed(string networkName, string adUnitId)
        {
            networkName = string.IsNullOrEmpty(networkName) ? "unknown" : networkName;

            GameAnalytics.NewAdEvent(GAAdAction.FailedShow, GAAdType.Interstitial, networkName, adUnitId);
        }

        internal static void LogInterstitialClicked(string networkName, string adUnitId)
        {
            networkName = string.IsNullOrEmpty(networkName) ? "unknown" : networkName;

            GameAnalytics.NewAdEvent(GAAdAction.Clicked, GAAdType.Interstitial, networkName, adUnitId);
        }


    }
}
