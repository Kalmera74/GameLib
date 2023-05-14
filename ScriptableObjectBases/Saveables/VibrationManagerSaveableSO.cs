using System.Collections;
using UnityEngine;

namespace GameLib.ScriptableObjectBases.Saveables
{
    [CreateAssetMenu(menuName = "Game/SaveManager/Saveable/VibrationSaveable", fileName = "Vibration_Save_Data")]
    public class VibrationManagerSaveableSO : SaveableSO
    {
        public bool CanVibrate = true;

    }
}