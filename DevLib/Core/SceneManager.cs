using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Mobiversite.GameLib.DevLib.Core.SaveSystem;
using UnityEditor;
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
        public SceneAsset Scene;
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

            LoadSceneAt(MenuSceneIndex, BootSceneLoadingMode);

            var lastPlayedLevel = SaveManager.Instance.GetLastLoadedScene();
            if (lastPlayedLevel == -1)
            {
                lastPlayedLevel = 0;
                SaveManager.Instance.SaveLastPlayedScene(lastPlayedLevel);
            }

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

        }
        public SceneDefinition GetCurrentSceneDefinition()
        {
            return NavigableScenes[CurrentlyLoadedSceneIndex];
        }
        public void LoadNavigableSceneAt(int sceneIndex, LoadSceneMode loadMode = LoadSceneMode.Single)
        {
            var realSceneIndex = ConvertFromNavigableToScenesIndex(sceneIndex);
            StartCoroutine(LoadSceneAsync(realSceneIndex, loadMode));
            SaveManager.Instance.SaveLastPlayedScene(sceneIndex);

        }
        public void LoadSceneAt(int sceneIndex, LoadSceneMode loadMode = LoadSceneMode.Single)
        {
            StartCoroutine(LoadSceneAsync(sceneIndex, loadMode));
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

            CurrentlyLoadedSceneIndex = ConvertFromScenesToNavigableIndex(sceneIndex);

        }

        private int ConvertFromScenesToNavigableIndex(int index)
        {
            var scene = Scenes[index];
            var convertedIndex = NavigableScenes.IndexOf(scene);
            return convertedIndex;
        }
        private int ConvertFromNavigableToScenesIndex(int index)
        {
            var scene = NavigableScenes[index];
            var convertedIndex = Scenes.IndexOf(scene);
            return convertedIndex;
        }
        private void SceneAsyncLoaded(AsyncOperation operation)
        {
            OnAfterLevelLoaded?.Invoke();
        }

        public void LoadNextLevel(LoadSceneMode loadMode = LoadSceneMode.Single)
        {
            int nextSceneIndex = CurrentlyLoadedSceneIndex + 1;

            if (nextSceneIndex >= TotalLoadableSceneCount)
            {
                nextSceneIndex = 0;
            }

            LoadNavigableSceneAt(nextSceneIndex, loadMode);
        }
        public void LoadPreviousLevel(LoadSceneMode loadMode = LoadSceneMode.Single)
        {
            int previousSceneIndex = CurrentlyLoadedSceneIndex - 1;
            if (previousSceneIndex < 0)
            {
                previousSceneIndex = NavigableScenes.Count - 1;
            }
            LoadNavigableSceneAt(previousSceneIndex, loadMode);
        }
        public void ReloadCurrentScene(LoadSceneMode loadMode = LoadSceneMode.Single)
        {
            LoadNavigableSceneAt(CurrentlyLoadedSceneIndex, loadMode);
        }

        public void LoadLastSavedLevel()
        {
            var lastLoadedLevelIndex = SaveManager.Instance.GetLastLoadedScene();
            LoadNavigableSceneAt(lastLoadedLevelIndex);
        }
    }
}
