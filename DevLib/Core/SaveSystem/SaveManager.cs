using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mobiversite.GameLib.DevLib.Core
{
    public class SaveManager : MonoBehaviour
    {
        [SerializeField] private Object ConcreteISaver;
        [SerializeField] private Object ConcreteILoader;

        private ISaver _saver;
        private ILoader _loader;
        private SaveDataObject _data;

        public static SaveManager Instance;

        void Awake()
        {
            if (ConcreteISaver is not null)
            {
                _saver = (ISaver)ConcreteISaver;
            }
            if (ConcreteILoader is not null)
            {
                _loader = (ILoader)ConcreteILoader;
            }

            if (Instance is null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                DestroyImmediate(gameObject);
            }

            LoadOrCreateSaveData();
        }

        public void SaveLastPlayedScene(int sceneIndex)
        {
            _data.LastPlayedLevel = sceneIndex;
            _saver.Save(_data);
        }
        public int GetLastLoadedScene()
        {
            return _data.LastPlayedLevel;
        }

        private void LoadOrCreateSaveData()
        {
            _data = _loader.Load();

            if (_data is null)
            {
                _data = new SaveDataObject();

            }
        }
    }
}
