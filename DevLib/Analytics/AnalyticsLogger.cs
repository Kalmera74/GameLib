using System.Collections;
using System.Collections.Generic;
using Firebase.Analytics;
using UnityEngine;

namespace Mobiversite.GameLib.DevLib.Analytics
{
    public static class AnalyticsLogger
    {


        public static void LogLevelLoadedEvent(int sceneIndex)
        {
            LogEvent("e_level_loaded", "p_scene_index", sceneIndex);
        }
        public static void LogLevelRestartEvent(int sceneIndex)
        {
            LogEvent("e_level_restarted", "p_scene_index", sceneIndex);

        }
        public static void LogWinnedEvent(int sceneIndex)
        {
            LogEvent("e_winned", "p_scene_index", sceneIndex);
        }

        public static void LogFailedEvent(int sceneIndex)
        {
            LogEvent("e_failed", "p_scene_index", sceneIndex);
        }
        public static void LogReplayedTheLevelEvent(int sceneIndex)
        {
            LogEvent("e_replayed", "p_scene_index", sceneIndex);
        }
        public static void LogLevelPlayTime(int sceneIndex, float playTime)
        {
            Firebase.Analytics.Parameter[] levelPlaytimeParameters =
                {
                    new Firebase.Analytics.Parameter("p_scene_index", sceneIndex),
                    new Firebase.Analytics.Parameter("p_play_time", playTime)
                };

            LogEvent("d_level_play_time", levelPlaytimeParameters);
        }
        private static void LogEvent(string eventName, string parameterName, string value)
        {
            FirebaseAnalytics.LogEvent(eventName, parameterName, value);
        }
        private static void LogEvent(string eventName, string parameterName, int value)
        {
            FirebaseAnalytics.LogEvent(eventName, parameterName, value);
        }
        private static void LogEvent(string eventName, string parameterName, float value)
        {
            FirebaseAnalytics.LogEvent(eventName, parameterName, value);
        }
        private static void LogEvent(string eventName, Parameter[] parameters)
        {
            FirebaseAnalytics.LogEvent(eventName, parameters);
        }
    }
}
