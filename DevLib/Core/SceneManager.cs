using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Mobiversite.GameLib.DevLib.Core.SaveSystem;
# if UNITY_EDITOR
using UnityEditor;
# endif
using UnityEngine;
using UnityEngine.SceneManagement;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

namespace Mobiversite.GameLib.DevLib.Core
{
    [Serializable]

    public enum SceneType
    {
        Boot,
        Normal,
        Menu
    }
    [Serializable]
    public struct SceneDefinition
    {
        public string LevelName;
#if UNITY_EDITOR
        public SceneAsset Scene;
#endif
        public SceneType SceneType;
    }

    public class SceneManager : MonoBehaviour
    {
        public static SceneManager Instance;
        private int TotalLoadableSceneCount = 0;
        private int CurrentlyLoadedSceneIndex = -1;
        private int BootSceneIndex = 0;
        private int MenuSceneIndex = 1;
        public event Action OnBeforeSceneLoaded;
        public event Action OnAfterLevelLoaded;
        public event Action<float> OnWhileLevelLoading;

        [SerializeField] private LoadSceneMode BootSceneLoadingMode = LoadSceneMode.Single;
        [SerializeField] private SceneType[] ExcludeFromLevelNavigation;
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

            var bootScene = Scenes.First(s => s.SceneType.Equals(SceneType.Boot));
            BootSceneIndex = Scenes.IndexOf(bootScene);

            var menuScene = Scenes.First(s => s.SceneType.Equals(SceneType.Menu));
            MenuSceneIndex = Scenes.IndexOf(menuScene);



            var lastPlayedLevel = SaveManager.Instance.GetLastLoadedScene();
            if (lastPlayedLevel == -1)
            {

                lastPlayedLevel = ConvertFromNavigableToScenesIndex(0);
                SaveManager.Instance.SaveLastPlayedScene(lastPlayedLevel);
            }
            CurrentlyLoadedSceneIndex = MenuSceneIndex;
            LoadSceneAt(MenuSceneIndex, BootSceneLoadingMode);
        }

        public void LoadScenesToList()
        {
#if UNITY_EDITOR

            List<EditorBuildSettingsScene> editorBuildSettingsScenes = new List<EditorBuildSettingsScene>();
            NavigableScenes.Clear();


            foreach (var sceneData in Scenes)
            {

                if (!ExcludeFromLevelNavigation.Contains(sceneData.SceneType))
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
#endif
        }
        public SceneDefinition GetCurrentSceneDefinition()
        {
            return Scenes[CurrentlyLoadedSceneIndex];
        }

        public void LoadSceneAt(int sceneIndex, LoadSceneMode loadMode = LoadSceneMode.Single)
        {
            StartCoroutine(LoadSceneAsync(sceneIndex, loadMode));
        }
        public int GetCurrentSceneIndex()
        {
            return CurrentlyLoadedSceneIndex;
        }
        public int GetNextSceneIndex(int index)
        {
            var nextSceneIndex = ConvertFromScenesToNavigableIndex(index) + 1;
            if (nextSceneIndex >= TotalLoadableSceneCount)
            {
                nextSceneIndex = 0;
            }
            return ConvertFromNavigableToScenesIndex(nextSceneIndex);
        }
        public int GetNextSceneIndex()
        {
            var nextSceneIndex = ConvertFromScenesToNavigableIndex(CurrentlyLoadedSceneIndex) + 1;
            if (nextSceneIndex >= TotalLoadableSceneCount)
            {
                nextSceneIndex = 0;
            }
            return ConvertFromNavigableToScenesIndex(nextSceneIndex);
        }
        private IEnumerator LoadSceneAsync(int sceneIndex, LoadSceneMode loadMode = LoadSceneMode.Single)
        {
            OnBeforeSceneLoaded?.Invoke();

            var asyncHandler = UnitySceneManager.LoadSceneAsync(sceneIndex, loadMode);

            asyncHandler.completed += SceneAsyncLoaded;

            while (!asyncHandler.isDone)
            {
                OnWhileLevelLoading?.Invoke(asyncHandler.progress);
                yield return null;
            }

            CurrentlyLoadedSceneIndex = sceneIndex;
            SaveLastLoadedNavigableScene();

        }

        private void SaveLastLoadedNavigableScene()
        {
            if (!ExcludeFromLevelNavigation.Contains(GetCurrentSceneDefinition().SceneType))
            {
                SaveManager.Instance.SaveLastPlayedScene(CurrentlyLoadedSceneIndex);
            }
        }

        private int ConvertFromScenesToNavigableIndex(int index)
        {
            var scene = Scenes[index];
            var convertedIndex = NavigableScenes.IndexOf(scene);
            if (convertedIndex == -1)
            {
                convertedIndex = 0;
            }
            return convertedIndex;
        }
        private int ConvertFromNavigableToScenesIndex(int index)
        {
            var scene = NavigableScenes[index];
            var convertedIndex = Scenes.IndexOf(scene);
            if (convertedIndex == -1)
            {
                convertedIndex = 1;
            }
            return convertedIndex;
        }
        private void SceneAsyncLoaded(AsyncOperation operation)
        {
            OnAfterLevelLoaded?.Invoke();
        }

        public void LoadNextLevel(LoadSceneMode loadMode = LoadSceneMode.Single)
        {
            int nextSceneIndex = ConvertFromScenesToNavigableIndex(CurrentlyLoadedSceneIndex) + 1;

            if (nextSceneIndex >= TotalLoadableSceneCount)
            {
                nextSceneIndex = 0;
            }
            nextSceneIndex = ConvertFromNavigableToScenesIndex(nextSceneIndex);
            LoadSceneAt(nextSceneIndex, loadMode);
        }
        public void LoadPreviousLevel(LoadSceneMode loadMode = LoadSceneMode.Single)
        {
            int previousSceneIndex = ConvertFromScenesToNavigableIndex(CurrentlyLoadedSceneIndex) - 1;
            if (previousSceneIndex < 0)
            {
                previousSceneIndex = NavigableScenes.Count - 1;
            }
            previousSceneIndex = ConvertFromNavigableToScenesIndex(previousSceneIndex);
            LoadSceneAt(previousSceneIndex, loadMode);
        }
        public void ReloadCurrentScene(LoadSceneMode loadMode = LoadSceneMode.Single)
        {
            LoadSceneAt(CurrentlyLoadedSceneIndex, loadMode);
        }

        public void LoadLastSavedLevel()
        {
            var lastLoadedLevelIndex = SaveManager.Instance.GetLastLoadedScene();
            LoadSceneAt(lastLoadedLevelIndex);
        }
    }
}
