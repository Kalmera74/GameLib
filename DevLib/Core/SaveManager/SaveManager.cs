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
        public int GetCurrencyAmount()
        {
            return _data.CurrencyAmount;
        }

        public int GetLastLoadedScene()
        {
            return _data.LastPlayedLevel;
        }

        public bool GetSFXState()
        {
            return _data.IsSFXOn;
        }

        public bool GetMusicMute()
        {
            return _data.IsMusicOn;
        }

        public float GetVolume()
        {
            return _data.Volume;
        }
        public bool GetVibrationState()
        {
            return _data.CanVibrate;
        }



        public void SaveLevelStates(List<LevelStateDefinition> states)
        {
            _data.LevelStates = states;
            Save();
        }

        public void SaveVibrationState(bool state)
        {
            _data.CanVibrate = state;
            Save();
        }
        public void SaveMusicState(bool state)
        {
            _data.IsMusicOn = state;
            Save();
        }
        public void SaveVolume(float t)
        {
            _data.Volume = t;
            Save();
        }
        public void SaveSFXState(bool state)
        {
            _data.IsSFXOn = state;
            Save();
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


        private void Save()
        {
            _saver.Save(_data);
        }

        public void SaveTapTutorialState(bool canPlayTapTutorial)
        {
            _data.CanPlayTapTutorial = canPlayTapTutorial;
            Save();
        }

        public bool GetCanPlayTapTutorial()
        {
            return _data.CanPlayTapTutorial;
        }

        public bool GetCanPlaySliderTutorial()
        {
            return _data.CanPlaySliderTutorial;
        }

        public void SaveSliderTutorialState(bool canPlayTapTutorial)
        {
            _data.CanPlaySliderTutorial = canPlayTapTutorial;
            Save();
        }

        public void SaveTapTwoTutorialState(bool canPlayTapTutorial)
        {
            _data.CanPlayTapTwoTutorial = canPlayTapTutorial;
        }

        public bool GetCanPlayTapTwoTutorial()
        {
            return _data.CanPlayTapTwoTutorial;
        }
    }
}
