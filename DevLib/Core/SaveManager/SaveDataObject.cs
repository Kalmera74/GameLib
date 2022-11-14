using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mobiversite.GameLib.DevLib.Core
{
    public class SaveDataObject
    {
        public int LastPlayedLevel = -1;
        public int CurrencyAmount = 0;

        public bool IsMuted = false;
        public float Volume = 1f;
    }
}
