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
        public bool IsSFXOn = true;
        public bool IsMusicOn = true;
        public float Volume = 1f;
        public bool CanVibrate = true;
        public bool CanPlayTapTutorial = true;
        public bool CanPlaySliderTutorial = true;
        public bool CanPlayTapTwoTutorial = true;
        public List<LevelStateDefinition> LevelStates = new List<LevelStateDefinition>();

    }
}
