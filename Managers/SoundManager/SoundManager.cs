using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Mobiversite
{
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] private AudioMixer AudioMixer;
        [SerializeField] private FloatEventDelegateSO SetMusicVolumeRequest;
        [SerializeField] private FloatEventDelegateSO SetSFXVolumeRequest;
        [SerializeField] private BooleanEventDelegateSO ChangeMusicStateRequest;
        [SerializeField] private BooleanEventDelegateSO ChangeSFXStateRequest;

        const string MASTER_VOLUME_KEY = "Master_Volume";
        const string MUSIC_VOLUME_KEY = "Music_Volume";
        const string SFX_VOLUME_KEY = "SFX_Volume";

        void Awake()
        {
            SetMusicVolumeRequest.Subscribe(SetMusicVolume);
            SetSFXVolumeRequest.Subscribe(SetSFXVolume);

            ChangeMusicStateRequest.Subscribe(SetMusicState);
            ChangeSFXStateRequest.Subscribe(SetSFXState);
        }

        private void SetSFXState(bool isActive)
        {
            if (isActive)
            {
                AudioMixer.ClearFloat(SFX_VOLUME_KEY);
            }
            else
            {
                SetSFXVolume(0);
            }
        }

        private void SetMusicState(bool isActive)
        {
            if (isActive)
            {
                AudioMixer.ClearFloat(MUSIC_VOLUME_KEY);
            }
            else
            {
                SetMusicVolume(0);
            }
        }

        private void SetSFXVolume(float volume)
        {
            float logarithmicVolume = ConvertVolumeToLogarithmicForm(volume);
            AudioMixer.SetFloat(SFX_VOLUME_KEY, logarithmicVolume);
        }


        private void SetMusicVolume(float volume)
        {
            float logarithmicVolume = ConvertVolumeToLogarithmicForm(volume);
            AudioMixer.SetFloat(MUSIC_VOLUME_KEY, logarithmicVolume);
        }

        private static float ConvertVolumeToLogarithmicForm(float volume)
        {
            volume = Mathf.Clamp(volume, 0.0001f, 1f);
            float logarithmicVolume = Mathf.Log10(volume) * 20;
            return logarithmicVolume;
        }
        void OnDestroy()
        {
            SetMusicVolumeRequest.UnSubscribe(SetMusicVolume);
            SetSFXVolumeRequest.UnSubscribe(SetSFXVolume);
            ChangeMusicStateRequest.UnSubscribe(SetMusicState);
            ChangeSFXStateRequest.UnSubscribe(SetSFXState);
        }

    }
}
