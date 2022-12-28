using System.Collections;
using UnityEngine;

namespace GameLib.ScriptableObjectBases.Saveables
{
    [CreateAssetMenu(menuName = "Mobiversite/SaveManager/Saveable/SceneSaveable", fileName = "Default_Scene_Saveable")]
    public class SceneManagerSaveableSO : SaveableSO
    {
        public int LastLoadedSceneIndex = -1;

    }
}