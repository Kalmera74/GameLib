using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Lofelt.NiceVibrations;
using Mobiversite.GameLib.DevLib.Core;
using UnityEngine;

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
        public void SetCanVibrate(bool canVibrate)
        {
            _canVibrate = canVibrate;
            Save();
        }
        public void PlayHaptic(HapticPatterns.PresetType pattern)
        {

            if (!_canVibrate)
            {
                return;
            }
            Play(pattern);
        }




        private void Play(HapticPatterns.PresetType pattern)
        {

            Source.fallbackPreset = pattern;
            Source.Play();

        }

        private void Save()
        {
            SaveManager.Instance.SaveCanVibrate(_canVibrate);
        }
    }
}
