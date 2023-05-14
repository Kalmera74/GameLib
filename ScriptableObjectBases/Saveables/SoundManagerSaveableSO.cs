using System.Collections;
using UnityEngine;

namespace GameLib.ScriptableObjectBases.Saveables
{
    [CreateAssetMenu(menuName = "Game/SaveManager/Saveable/SoundSaveable", fileName = "Sound_Save_Data")]
    public class SoundManagerSaveableSO : SaveableSO
    {
        public bool IsSFXActive = true;
        public bool IsMusicActive = true;

    }
}