using System;
using System.Collections;
using System.Collections.Generic;
using Mobiversite.GameLib.DevLib.Core;
using UnityEngine;

namespace Mobiversite.GameLib.DevLib.Settings
{
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] private AudioSource Audio;
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
        void Start()
        {
            bool isMute = SaveManager.Instance.GetIsMute();
            AudioListener.pause = isMute;
        }
        public void SetMute(bool isMute)
        {
            AudioListener.pause = isMute;
            Save();
        }
        public bool GetIsMute()
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
        public void Play(AudioClip clip)
        {
            Audio.clip = clip;
            Audio.Play();
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
