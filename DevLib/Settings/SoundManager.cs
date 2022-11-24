using System;
using System.Collections;
using System.Collections.Generic;
using Mobiversite.GameLib.DevLib.Core;
using UnityEngine;

namespace Mobiversite.GameLib.DevLib.Settings
{
    public class SoundManager : MonoBehaviour
    {
        public event Action OnSettingChanged;
        private float _volumeBeforeMuting;

        public static SoundManager Instance;

        void Awake()
        {
            if (Instance is null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                DestroyImmediate(gameObject);
            }

            var isMuted = SaveManager.Instance.GetIsMute();
            AudioListener.pause = isMuted;

            var volume = SaveManager.Instance.GetVolume();
            SetVolume(volume);
        }
        public void SetMute(bool isMute)
        {
            AudioListener.pause = isMute;
            Save();
        }
        public bool IsMute()
        {
            return AudioListener.pause;
        }
        public void SetVolume(float t)
        {
            if (t < 0f || t > 1f)
            {
                return;
            }

            AudioListener.volume = t;
            Save();
        }
        private void Save()
        {
            SaveManager.Instance.SaveIsMute(AudioListener.pause);
            SaveManager.Instance.SaveVolume(AudioListener.volume);
            FireEvent();
        }
        private void FireEvent()
        {
            OnSettingChanged?.Invoke();
        }

    }
}
