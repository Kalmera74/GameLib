using System.Collections;
using System.Collections.Generic;
using Mobiversite.Assets._Project.Scripts;
using UnityEngine;

namespace Mobiversite.GameLib.DevLib.Core
{
    public class SaveDataObject
    {
        public int LastPlayedLevel = -1;
        public int CurrencyAmount = 0;
        public bool IsMuted = false;
        public float Volume = 1f;
        public bool CanVibrate = true;
        public List<LevelStateDefinition> LevelStates = new List<LevelStateDefinition>();
    }
}
