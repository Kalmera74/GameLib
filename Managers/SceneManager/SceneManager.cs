using GameLib.ScriptableObjectBases.EventDelegates;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameLib.ScriptableObjectBases.Saveables;
using GameLib.ScriptableObjectBases.SceneCollection;
using GameLib.ScriptableObjectBases.PrimitiveReferences;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace GameLib.Managers.SceneManager
{
    /// <summary>
    /// A struct that represents a scene definition, including the name and index of the scene.
    /// </summary>
    [Serializable]
    public struct SceneDefinition
    {
        /// <summary>
        /// The name of the scene.
        /// </summary>
        public string SceneName;

        /// <summary>
        /// The index of the scene.
        /// </summary>
        public int SceneIndex;

#if UNITY_EDITOR
        /// <summary>
        /// A reference to the scene asset in the Unity Editor.
        /// This field is only available in the Unity Editor.
        /// </summary>
        public SceneAsset Scene;
#endif


    }
    /// <summary>
    /// The DefaultSceneManager is a class that manages the loading of scenes in a game.
    /// It provides methods for loading the next, previous, or a specific level, as well as reloading the current level.
    /// </summary>
    [Serializable]
    public class SceneManager : MonoBehaviour
    {
        /// <summary>
        /// A scene collection that contains the definitions of the scenes in the game.
        /// </summary>
        [SerializeField] private SceneCollectionSO SceneCollection;
        /// <summary>
        /// An event delegate that triggers before a scene is loaded.
        /// </summary>
        [SerializeField] private VoidEventDelegateSO OnBeforeLevelLoadEvent;
        /// <summary>
        /// An event delegate that triggers while a scene is being loaded.
        /// </summary>
        [SerializeField] private VoidEventDelegateSO OnWhileLevelLoadingEvent;
        /// <summary>
        /// An event delegate that triggers after a scene has been loaded.
        /// </summary>
        [SerializeField] private VoidEventDelegateSO OnAfterLevelLoadedEvent;
        /// <summary>
        /// A reference to the current index of the scene in the scene collection.
        /// </summary>
        [SerializeField] private IntRefSO _currentIndexPrimitiveRef;
        /// <summary>
        /// The minimum index of the scene collection.
        /// </summary>
        private int _minimumIndex = 0;
        /// <summary>
        /// The maximum index of the scene collection.
        /// </summary>
        private int _maxIndex = 0;

        /// <summary>
        /// An event delegate that triggers when a request to load the next level is made.
        /// </summary>
        [SerializeField] private VoidEventDelegateSO NextLevelLoadRequest;

        /// <summary>
        /// An event delegate that triggers when a request to load the previous level is made.
        /// </summary>
        [SerializeField] private VoidEventDelegateSO PreviousLEvelLoadRequest;

        /// <summary>
        /// An event delegate that triggers when a request to reload the current level is made.
        /// </summary>
        [SerializeField] private VoidEventDelegateSO ReloadCurrentLevelRequest;

        /// <summary>
        /// An event delegate that triggers when a request to load a specific level is made.
        /// </summary>
        [SerializeField] private EventDelegateSO<int> LoadLevelRequest;

        /// <summary>
        /// A saveable object that stores data about the current state of the scene manager.
        /// </summary>
        [SerializeField] private SceneManagerSaveableSO SceneData;

        /// <summary>
        /// An event delegate that triggers when a request to save the current state of the scene manager is made.
        /// </summary>
        [SerializeField] private VoidEventDelegateSO SaveRequestDelegate;
        private LoadSceneMode _sceneLoadMode = LoadSceneMode.Single;

        /// <summary>
        /// Initializes the DefaultSceneManager by subscribing to the various event delegates and setting the maximum and minimum indices.
        /// </summary>
        private void Awake()
        {
            LoadLevelRequest.Subscribe(LoadSceneAt);
            NextLevelLoadRequest.Subscribe(LoadNextLevel);
            PreviousLEvelLoadRequest.Subscribe(LoadPreviousLevel);
            ReloadCurrentLevelRequest.Subscribe(ReloadCurrentLevel);

            _maxIndex = SceneCollection.GetLevelCount();
            _currentIndexPrimitiveRef.SetValue(0);
        }

        void Start()
        {
            _sceneLoadMode = LoadSceneMode.Additive;
            LoadNextLevel();
            _sceneLoadMode = LoadSceneMode.Single;
        }
        /// <summary>
        /// Loads the scene at the specified level index.
        /// </summary>
        /// <param name="levelIndex">The index of the level to load.</param>
        private void LoadSceneAt(int levelIndex)
        {
            StartCoroutine(LoadSceneAsync(levelIndex));
        }

        /// <summary>
        /// Loads the scene at the specified level index asynchronously
        /// <summary>
        /// Loads the scene at the specified level index asynchronously.
        /// </summary>
        /// <param name="levelIndex">The index of the level to load.</param>
        /// <returns>An enumerator for the coroutine.</returns>
        private IEnumerator LoadSceneAsync(int levelIndex)
        {
            OnBeforeLevelLoadEvent.FireEvent();

            int sceneIndex = SceneCollection.GetItemAt(levelIndex).SceneIndex;


            var asyncHandler = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneIndex, _sceneLoadMode);

            while (!asyncHandler.isDone)
            {
                OnWhileLevelLoadingEvent.FireEvent();
                yield return null;
            }
            _currentIndexPrimitiveRef.SetValue(levelIndex);

            OnAfterLevelLoadedEvent.FireEvent();
        }

        /// <summary>
        /// Loads the next level in the scene collection.
        /// If the next level is the last level, it will loop back to the first level.
        /// </summary>
        private void LoadNextLevel()
        {
            int nextIndex = _currentIndexPrimitiveRef.GetValue() + 1;
            int nextSceneIndex = SceneCollection.GetItemAt(nextIndex).SceneIndex;

            if (nextSceneIndex >= _maxIndex)
            {
                nextSceneIndex = 0;
            }

            LoadSceneAt(nextSceneIndex);
        }

        /// <summary>
        /// Loads the previous level in the scene collection.
        /// If the previous level is the first level, it will loop back to the last level.
        /// </summary>
        private void LoadPreviousLevel()
        {
            int previousSceneIndex = SceneCollection.GetItemAt(_currentIndexPrimitiveRef.GetValue() - 1).SceneIndex;
            if (previousSceneIndex < _minimumIndex)
            {
                previousSceneIndex = SceneCollection.GetLastItem().SceneIndex;
            }

            LoadSceneAt(previousSceneIndex);
        }

        /// <summary>
        /// Reloads the current level.
        /// </summary>
        private void ReloadCurrentLevel()
        {
            LoadSceneAt(_currentIndexPrimitiveRef.GetValue());
        }

        /// <summary>
        /// Unsubscribes from the event delegates when the DefaultSceneManager is destroyed.
        /// </summary>
        private void OnDestroy()
        {
            LoadLevelRequest.UnSubscribe(LoadSceneAt);
            NextLevelLoadRequest.UnSubscribe(LoadNextLevel);
            PreviousLEvelLoadRequest.UnSubscribe(LoadPreviousLevel);
            ReloadCurrentLevelRequest.UnSubscribe(ReloadCurrentLevel);
        }

        /// <summary>
        /// Loads the scenes in the SceneCollection into the build settings list.
        /// This method is only available in the Unity Editor.
        /// </summary>
        public void LoadScenesToList()
        {
#if UNITY_EDITOR
            List<EditorBuildSettingsScene> editorBuildSettingsScenes = new List<EditorBuildSettingsScene>();

            int length = SceneCollection.GetLevelCount();

            for (int i = 0; i < length; i++)
            {
                SceneDefinition sceneData = SceneCollection.GetItemAt(i);

                string scenePath = AssetDatabase.GetAssetPath(sceneData.Scene);

                if (!string.IsNullOrEmpty(scenePath))
                {
                    editorBuildSettingsScenes.Add(new EditorBuildSettingsScene(scenePath, true));
                }
            }

            EditorBuildSettings.scenes = editorBuildSettingsScenes.ToArray();

            EditorUtility.SetDirty(this);

            UnityEditor.SceneManagement.EditorSceneManager.SaveOpenScenes();
#endif
        }

        /// <summary>
        /// Saves the current state of the DefaultSceneManager.
        /// </summary>
        private void Save()
        {

            SaveRequestDelegate.FireEvent();
        }
    }
}

