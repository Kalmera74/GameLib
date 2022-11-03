using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

namespace Mobiversite.GameLib.DevLib.Core
{
    [Serializable]
    [Flags]
    public enum SceneType
    {
        Boot = 1,
        Normal
    }
    [Serializable]
    public struct SceneDefinition
    {
        public string LevelName;
        public SceneAsset Scene;
        public SceneType SceneType;
    }
    public class SceneManager : MonoBehaviour
    {
        public static SceneManager Instance;
        private int TotalLoadableSceneCount = 0;
        private int CurrentlyLoadedSceneIndex = 0;

        public event Action OnBeforeSceneLoaded;
        public event Action OnAfterLevelLoaded;

        [SerializeField] private LoadSceneMode BootSceneLoadingMode = LoadSceneMode.Single;
        [SerializeField] private SceneType ExcludeFromLevelNavigation;
        [SerializeField] private List<SceneDefinition> Scenes = new List<SceneDefinition>();
        [SerializeField] private List<SceneDefinition> NavigableScenes = new List<SceneDefinition>();

        void Awake()
        {
            if (Instance == null)
            {

                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                DestroyImmediate(gameObject);
            }

        }
        void Start()
        {
            TotalLoadableSceneCount = NavigableScenes.Count;
            LoadNextLevel(BootSceneLoadingMode);
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.RightShift))
            {
                LoadNextLevel();
            }

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                LoadPreviousLevel();
            }

        }
        public void LoadScenesToList()
        {

            List<EditorBuildSettingsScene> editorBuildSettingsScenes = new List<EditorBuildSettingsScene>();
            NavigableScenes.Clear();


            foreach (var sceneData in Scenes)
            {
                if (!sceneData.SceneType.HasFlag(ExcludeFromLevelNavigation))
                {
                    NavigableScenes.Add(sceneData);
                }

                string scenePath = AssetDatabase.GetAssetPath(sceneData.Scene);

                if (!string.IsNullOrEmpty(scenePath))
                {
                    editorBuildSettingsScenes.Add(new EditorBuildSettingsScene(scenePath, true));
                }
            }

            EditorBuildSettings.scenes = editorBuildSettingsScenes.ToArray();

            EditorUtility.SetDirty(this);

        }

        public void LoadSceneAt(int sceneIndex, LoadSceneMode loadMode = LoadSceneMode.Single)
        {
            var convertedIndex = ConvertToNavigableIndex(sceneIndex);

            OnBeforeSceneLoaded?.Invoke();

            UnitySceneManager.LoadScene(convertedIndex, loadMode);
            CurrentlyLoadedSceneIndex = sceneIndex;

            OnAfterLevelLoaded?.Invoke();
        }

        private int ConvertToNavigableIndex(int sceneIndex)
        {

            var proxyScene = NavigableScenes[sceneIndex];
            var intendedScene = Scenes.IndexOf(proxyScene);
            return intendedScene;
        }

        public void LoadNextLevel(LoadSceneMode loadMode = LoadSceneMode.Single)
        {

            int nextSceneIndex = CurrentlyLoadedSceneIndex + 1;

            if (nextSceneIndex >= TotalLoadableSceneCount)
            {
                nextSceneIndex = 0;
            }

            LoadSceneAt(nextSceneIndex, loadMode);
        }
        public void LoadPreviousLevel(LoadSceneMode loadMode = LoadSceneMode.Single)
        {
            int previousSceneIndex = CurrentlyLoadedSceneIndex - 1;
            if (previousSceneIndex < 0)
            {
                previousSceneIndex = NavigableScenes.Count - 1;
            }
            LoadSceneAt(previousSceneIndex, loadMode);
        }
        public void ReloadCurrentScene(LoadSceneMode loadMode = LoadSceneMode.Single)
        {
            LoadSceneAt(CurrentlyLoadedSceneIndex, loadMode);
        }
    }
}
