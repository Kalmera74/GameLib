using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

namespace Mobiversite.GameLib.DevLib.Core
{
    [Serializable]
    public enum SceneType
    {
        Normal
    }
    [Serializable]
    public struct SceneDefinition
    {
        public string SceneName;
        public UnityEngine.SceneManagement.Scene Scene;
        public SceneType SceneType;
    }
    public class SceneManager : MonoBehaviour
    {
        public static SceneManager Instance;
        [SerializeField] private int TotalLoadableSceneCount = 0;
        [SerializeField] private int CurrentlyLoadedSceneIndex = 0;
        [SerializeField] private List<SceneDefinition> Scenes = new List<SceneDefinition>();

        void Awake()
        {
            Instance = this;
        }
        public void LoadScenesToList()
        {

            Scenes.Clear();
            TotalLoadableSceneCount = 0;

            int totalSceneCount = UnitySceneManager.sceneCount;

            for (int i = 0; i < totalSceneCount; i++)
            {
                var scene = UnitySceneManager.GetSceneByBuildIndex(0);

                var definedScene = new SceneDefinition();
                definedScene.SceneName = scene.name;
                definedScene.Scene = scene;
                definedScene.SceneType = SceneType.Normal;

                Scenes.Add(definedScene);

                TotalLoadableSceneCount++;
            }
            Debug.Log($"!!! {Scenes.Count} Scene Loaded");
        }

        public void LoadSceneAt(int sceneIndex, LoadSceneMode loadMode = LoadSceneMode.Single)
        {
            UnitySceneManager.LoadScene(sceneIndex, loadMode);
        }
        public void LoadNextLevel(LoadSceneMode loadMode = LoadSceneMode.Single)
        {
            int nextSceneIndex = CurrentlyLoadedSceneIndex + 1;
            if (nextSceneIndex <= TotalLoadableSceneCount)
            {
                LoadSceneAt(nextSceneIndex, loadMode);
            }
        }
        public void LoadPreviousLevel(LoadSceneMode loadMode = LoadSceneMode.Single)
        {
            int previousSceneIndex = CurrentlyLoadedSceneIndex - 1;
            if (previousSceneIndex >= 0)
            {
                LoadSceneAt(previousSceneIndex, loadMode);
            }
        }
    }
}
