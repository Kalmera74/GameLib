using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Lofelt.NiceVibrations;
using Mobiversite.GameLib.DevLib.Core;
using UnityEngine;
using static Lofelt.NiceVibrations.HapticPatterns;

namespace Mobiversite.GameLib.DevLib.Settings
{

    public class VibrationManager : MonoBehaviour
    {

        [SerializeField] private HapticSource Source;
        private bool _canVibrate = true;

        public static VibrationManager Instance;
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
            _canVibrate = SaveManager.Instance.GetVibrationState();
        }
        public void SetCanVibrate(bool canVibrate)
        {
            _canVibrate = canVibrate;
            Save();
        }
        public void PlayHaptic(PresetType pattern)
        {

            if (!_canVibrate)
            {
                return;
            }
            Play(pattern);
        }

        public bool GetCanVibrate()
        {
            return _canVibrate;
        }
        public void PlaySelectionHaptic()
        {

            PlayHaptic(PresetType.Selection);
        }
        public void PlaySuccessHaptic()
        {
            PlayHaptic(PresetType.Success);
        }
        public void PlayWarningHaptic()
        {
            PlayHaptic(PresetType.Warning);
        }
        public void PlayFailure()
        {
            PlayHaptic(PresetType.Failure);
        }
        public void PlayLightImpact()
        {
            PlayHaptic(PresetType.LightImpact);
        }
        public void PlayMediumImpact()
        {
            PlayHaptic(PresetType.MediumImpact);
        }
        public void PlayHeavyImpact()
        {
            PlayHaptic(PresetType.HeavyImpact);
        }
        public void PlayRigidImpact()
        {
            PlayHaptic(PresetType.RigidImpact);
        }
        public void PlaySoftImpact()
        {
            PlayHaptic(PresetType.SoftImpact);
        }
        private void Play(PresetType pattern)
        {

            Source.fallbackPreset = pattern;
            Source.Play();

        }

        private void Save()
        {
            SaveManager.Instance.SaveVibrationState(_canVibrate);
        }
    }
}
