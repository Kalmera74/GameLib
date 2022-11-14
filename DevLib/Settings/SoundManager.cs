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

        void Awake()
        {
            var isMuted = SaveManager.Instance.GetIsMute();
            AudioListener.pause = isMuted;

            var volume = SaveManager.Instance.GetVolume();
            SetVolume(volume);
        }
        public void Mute()
        {
            AudioListener.pause = true;
        }
        public void UnMute()
        {
            AudioListener.pause = false;
        }
        public void SetVolume(float t)
        {
            if (t < 0f || t > 1f)
            {
                return;
            }

            AudioListener.volume = t;
        }

        private void FireEvent()
        {
            OnSettingChanged?.Invoke();
        }

    }
}
