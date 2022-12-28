using GameLib.ScriptableObjectBases.EventDelegates;
using GameLib.ScriptableObjectBases.Saveables;
using UnityEngine;
using UnityEngine.Audio;

namespace GameLib.Managers.SoundManager
{
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] private AudioMixer AudioMixer;
        [SerializeField] private FloatEventDelegateSO SetMusicVolumeRequest;
        [SerializeField] private FloatEventDelegateSO SetSFXVolumeRequest;
        [SerializeField] private BooleanEventDelegateSO ChangeMusicStateRequest;
        [SerializeField] private BooleanEventDelegateSO ChangeSFXStateRequest;
        [SerializeField] private SoundManagerSaveableSO SoundData;
        [SerializeField] private VoidEventDelegateSO SaveRequestDelegate;
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
            SoundData.IsSFXActive = isActive;
            if (SoundData.IsSFXActive)
            {
                AudioMixer.ClearFloat(SFX_VOLUME_KEY);
            }
            else
            {
                SetSFXVolume(0);
            }
            Save();
        }

        private void SetMusicState(bool isActive)
        {
            SoundData.IsMusicActive = isActive;
            if (SoundData.IsMusicActive)
            {
                AudioMixer.ClearFloat(MUSIC_VOLUME_KEY);
            }
            else
            {
                SetMusicVolume(0);
            }
            Save();
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

        private void Save()
        {
            SaveRequestDelegate?.FireEvent();
        }
    }
}
