using System;
using System.Collections;
using System.Collections.Generic;
using Mobiversite.GameLib.DevLib.Core;
using UnityEngine;

namespace Mobiversite.GameLib.DevLib.Settings
{
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] private AudioSource SFXAudioPlayer;
        [SerializeField] private AudioSource MusicPlayer;

        private bool _isSFXOn = false;
        private bool _isMusicOn = false;
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


        }
        void Start()
        {
            _isSFXOn = SaveManager.Instance.GetSFXState();
            SFXAudioPlayer.mute = !_isSFXOn;

            _isMusicOn = SaveManager.Instance.GetMusicMute();
            MusicPlayer.mute = !_isMusicOn;

            var volume = SaveManager.Instance.GetVolume();
            SetVolume(volume);
        }
        public void SetSFXState(bool state)
        {
            _isSFXOn = state;
            SFXAudioPlayer.mute = !_isSFXOn;
            Save();
        }
        public void SetMusicState(bool state)
        {
            _isMusicOn = state;
            MusicPlayer.mute = !_isMusicOn;
            Save();
        }
        public bool GetIsSFXOn()
        {
            return _isSFXOn;
        }
        public bool GetIsMusicOn()
        {
            return _isMusicOn;
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
        public float GetVolume()
        {
            return AudioListener.volume;
        }
        public void PlaySFX(AudioClip clip)
        {
            SFXAudioPlayer.clip = clip;
            SFXAudioPlayer.Play();
        }
        public void PlayMusic(AudioClip clip)
        {
            MusicPlayer.clip = clip;
            // TODO add fade-out,fade-in between old and new clip with settings
            MusicPlayer.Play();
        }
        private void Save()
        {
            SaveManager.Instance.SaveSFXState(_isSFXOn);
            SaveManager.Instance.SaveMusicState(_isMusicOn);
            SaveManager.Instance.SaveVolume(AudioListener.volume);

        }
    }
}
