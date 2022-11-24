using System.Collections;
using System.Collections.Generic;
using Mobiversite.GameLib.DevLib.Core;
using UnityEngine;

namespace Mobiversite.GameLib.DevLib.Settings
{
    public class VibrationManager : MonoBehaviour
    {
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

        private void Save()
        {
            SaveManager.Instance.SaveCanVibrate(_canVibrate);
        }
    }
}
