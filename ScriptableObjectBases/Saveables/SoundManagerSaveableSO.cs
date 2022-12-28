using System.Collections;
using UnityEngine;

namespace GameLib.ScriptableObjectBases.Saveables
{
    [CreateAssetMenu(menuName = "Mobiversite/SaveManager/Saveable/SceneSaveable", fileName = "Default_Scene_Saveable")]
    public class SoundManagerSaveableSO : SaveableSO
    {
        public bool IsSFXActive = true;
        public bool IsMusicActive = true;

    }
}