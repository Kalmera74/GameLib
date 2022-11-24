using System.Collections;
using System.Collections.Generic;
using Mobiversite.Assets._Project.Scripts;
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

        public int GetCurrencyAmount()
        {
            return _data.CurrencyAmount;
        }
        public void SaveCurrencyAmount(int currencyAmount)
        {
            _data.CurrencyAmount = currencyAmount;
            Save();
        }


        public void SaveLastPlayedScene(int sceneIndex)
        {
            _data.LastPlayedLevel = sceneIndex;
            Save();
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
                Save();
            }
        }
        public List<LevelStateDefinition> GetLevelStates()
        {
            return _data.LevelStates;

        }
        private void Save()
        {
            _saver.Save(_data);
        }

        public bool GetIsMute()
        {
            return _data.IsMuted;
        }
        public void SaveIsMute(bool isMuted)
        {
            _data.IsMuted = isMuted;
            Save();
        }
        public void SaveVolume(float t)
        {
            _data.Volume = t;
            Save();
        }
        public float GetVolume()
        {
            return _data.Volume;
        }

        public void SaveLevelStates(List<LevelStateDefinition> states)
        {
            _data.LevelStates = states;
            Save();
        }

        public void SaveCanVibrate(bool canVibrate)
        {
            _data.CanVibrate = canVibrate;
            Save();
        }
    }
}
