using System.Collections;
using UnityEngine;

namespace GameLib.ScriptableObjectBases.Saveables
{
    [CreateAssetMenu(menuName = "Mobiversite/SaveManager/Saveable/VibrationSaveable", fileName = "Vibration_Save_Data")]
    public class VibrationManagerSaveableSO : SaveableSO
    {
        public bool CanVibrate = true;

    }
}