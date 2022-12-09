using GameLib.ScriptableObjectBases.EventDelegates;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace GameLib.Managers.SceneManager
{
    [Serializable]
    public class DefaultSceneManager : SceneManager<SceneDefinition>
    {
        [SerializeField] private VoidEventDelegateSO NextLevelLoadRequest;
        [SerializeField] private VoidEventDelegateSO PreviousLEvelLoadRequest;
        [SerializeField] private VoidEventDelegateSO ReloadCurrentLevelRequest;
        [SerializeField] private EventDelegateSO<int> LoadLevelRequest;
        protected override void Init()
        {

            LoadLevelRequest.Subscribe(LoadSceneAt);
            NextLevelLoadRequest.Subscribe(LoadNextLevel);
            PreviousLEvelLoadRequest.Subscribe(LoadPreviousLevel);
            ReloadCurrentLevelRequest.Subscribe(ReloadCurrentLevel);


            _maxIndex = SceneCollection.GetLevelCount() - 1;
            // ! Get the las saved/current index from save system
        }

        protected override void LoadSceneAt(int levelIndex)
        {
            StartCoroutine(LoadSceneAsync(levelIndex));
        }

        private IEnumerator LoadSceneAsync(int levelIndex)
        {
            OnBeforeLevelLoadEvent.FireEvent();

            int sceneIndex = SceneCollection.GetItemAt(levelIndex).SceneIndex;

            var asyncHandler = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneIndex);

            while (!asyncHandler.isDone)
            {
                OnWhileLevelLoadingEvent.FireEvent();
                yield return null;
            }
            _currentIndexPrimitiveRef.SetValue(levelIndex);
            OnAfterLevelLoadedEvent.FireEvent();
        }

        protected override void LoadNextLevel()
        {
            int nextSceneIndex = SceneCollection.GetItemAt(_currentIndexPrimitiveRef.GetValue() + 1).SceneIndex;

            if (nextSceneIndex >= _maxIndex)
            {
                nextSceneIndex = 0;
            }

            LoadSceneAt(nextSceneIndex);
        }

        protected override void LoadPreviousLevel()
        {
            int previousSceneIndex = SceneCollection.GetItemAt(_currentIndexPrimitiveRef.GetValue() - 1).SceneIndex;
            if (previousSceneIndex < _minimumIndex)
            {
                previousSceneIndex = SceneCollection.GetLastItem().SceneIndex;
            }

            LoadSceneAt(previousSceneIndex);
        }

        protected override void ReloadCurrentLevel()
        {
            LoadSceneAt(_currentIndexPrimitiveRef.GetValue());
        }

        private void OnDestroy()
        {

            LoadLevelRequest.UnSubscribe(LoadSceneAt);
            NextLevelLoadRequest.UnSubscribe(LoadNextLevel);
            PreviousLEvelLoadRequest.UnSubscribe(LoadPreviousLevel);
            ReloadCurrentLevelRequest.UnSubscribe(ReloadCurrentLevel);

        }

        public override void LoadScenesToList()
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


    }
}
