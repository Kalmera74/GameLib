using GameLib.ScriptableObjectBases.EventDelegates;
using GameLib.ScriptableObjectBases.Saveables;
using UnityEngine;
using UnityEngine.Audio;

namespace GameLib.Managers.SoundManager
{
    /// <summary>
    /// A component that manages the sound effects and music of the game.
    /// </summary>
    public class SoundManager : MonoBehaviour
    {
        /// <summary>
        /// The audio mixer used by the sound manager.
        /// </summary>
        [SerializeField] private AudioMixer AudioMixer;

        /// <summary>
        /// A scriptable object delegate that allows setting the music volume.
        /// </summary>
        [SerializeField] private FloatEventDelegateSO SetMusicVolumeRequest;

        /// <summary>
        /// A scriptable object delegate that allows setting the SFX volume.
        /// </summary>
        [SerializeField] private FloatEventDelegateSO SetSFXVolumeRequest;

        /// <summary>
        /// A scriptable object delegate that allows changing the music state (on/off).
        /// </summary>
        [SerializeField] private BooleanEventDelegateSO ChangeMusicStateRequest;

        /// <summary>
        /// A scriptable object delegate that allows changing the SFX state (on/off).
        /// </summary>
        [SerializeField] private BooleanEventDelegateSO ChangeSFXStateRequest;

        /// <summary>
        /// A scriptable object that holds the data for the sound manager.
        /// </summary>
        [SerializeField] private SoundManagerSaveableSO SoundData;

        /// <summary>
        /// A scriptable object delegate that allows saving the sound manager data.
        /// </summary>
        [SerializeField] private VoidEventDelegateSO SaveRequestDelegate;

        /// <summary>
        /// The key used to access the master volume in the audio mixer.
        /// </summary>
        const string MASTER_VOLUME_KEY = "Master_Volume";

        /// <summary>
        /// The key used to access the music volume in the audio mixer.
        /// </summary>
        const string MUSIC_VOLUME_KEY = "Music_Volume";

        /// <summary>
        /// The key used to access the SFX volume in the audio mixer.
        /// </summary>
        const string SFX_VOLUME_KEY = "SFX_Volume";

        /// <summary>
        /// Subscribes to the event delegates when the component awakens.
        /// </summary>
        void Awake()
        {
            SetMusicVolumeRequest.Subscribe(SetMusicVolume);
            SetSFXVolumeRequest.Subscribe(SetSFXVolume);

            ChangeMusicStateRequest.Subscribe(SetMusicState);
            ChangeSFXStateRequest.Subscribe(SetSFXState);
        }

        /// <summary>
        /// Sets the state of the SFX (on/off).
        /// </summary>
        /// <param name="isActive">Whether the SFX should be turned on or off.</param>
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

        /// <summary>
        /// Sets the state of the music (on/off).
        /// </summary>
        /// <param name="isActive">Whether the music should be turned on or off.</param>
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

        /// <summary>
        /// Sets the volume of the SFX.
        /// </summary>
        /// <param name="volume">The volume level, from 0 to 1.</param>
        private void SetSFXVolume(float volume)
        {
            float logarithmicVolume = ConvertVolumeToLogarithmicForm(volume);
            AudioMixer.SetFloat(SFX_VOLUME_KEY, logarithmicVolume);
        }

        /// <summary>
        /// Sets the volume of the music.
        /// </summary>
        /// <param name="volume">The volume level, from 0 to 1.</param>
        private void SetMusicVolume(float volume)
        {
            float logarithmicVolume = ConvertVolumeToLogarithmicForm(volume);
            AudioMixer.SetFloat(MUSIC_VOLUME_KEY, logarithmicVolume);
        }

        /// <summary>
        /// Converts a volume level from a linear scale (0 to 1) to a logarithmic scale (-80 to 20).
        /// </summary>
        /// <param name="volume">The volume level on a linear scale, from 0 to 1.</param>
        /// <returns>The volume level on a logarithmic scale, from -80 to 20.</returns>
        private static float ConvertVolumeToLogarithmicForm(float volume)
        {
            volume = Mathf.Clamp(volume, 0.0001f, 1f);
            float logarithmicVolume = Mathf.Log10(volume) * 20;
            return logarithmicVolume;
        }

        /// <summary>
        /// Unsubscribes from the event delegates when the component is destroyed.
        /// </summary>
        void OnDestroy()
        {
            SetMusicVolumeRequest.UnSubscribe(SetMusicVolume);
            SetSFXVolumeRequest.UnSubscribe(SetSFXVolume);
            ChangeMusicStateRequest.UnSubscribe(SetMusicState);
            ChangeSFXStateRequest.UnSubscribe(SetSFXState);
        }

        /// <summary>
        /// Saves the data for the sound manager.
        /// </summary>
        private void Save()
        {
            SaveRequestDelegate?.FireEvent();
        }
    }
}
