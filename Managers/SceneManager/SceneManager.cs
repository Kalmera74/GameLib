using System;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace Mobiversite
{
    [Serializable]
    public struct SceneDefinition
    {
        public string SceneName;
        public int SceneIndex;
#if UNITY_EDITOR
        public SceneAsset Scene;
#endif

    }

    [Serializable]
    public abstract class SceneManager<T> : MonoBehaviour
    {

        [SerializeField] protected SceneCollectionSO<T> SceneCollection;
        [SerializeField] protected VoidEventDelegateSO OnBeforeLevelLoadEvent;
        [SerializeField] protected VoidEventDelegateSO OnWhileLevelLoadingEvent;
        [SerializeField] protected VoidEventDelegateSO OnAfterLevelLoadedEvent;
        [SerializeField] protected IntRefSO _currentIndexPrimitiveRef;
        protected int _minimumIndex = 0;
        protected int _maxIndex = 0;

        protected abstract void Init();
        protected abstract void LoadSceneAt(int levelIndex);
        protected abstract void LoadNextLevel();
        protected abstract void LoadPreviousLevel();
        protected abstract void ReloadCurrentLevel();
        public abstract void LoadScenesToList();
    }
}
