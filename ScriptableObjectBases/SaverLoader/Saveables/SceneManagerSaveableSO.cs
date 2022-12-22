using System.Collections;
using UnityEngine;

namespace GameLib.ScriptableObjectBases.SaverLoader.Saveables
{
    [CreateAssetMenu(menuName = "Mobiversite/SaveManager/Saveable/SceneSaveable", fileName = "Default_Scene_Saveable")]
    public class SceneManagerSaveableSO : SaveableSO
    {
        public int LastLoadedSceneIndex = -1;

        public override void Deserialize(string data)
        {
            JsonUtility.FromJsonOverwrite(data,this);
        }

        public override string Serialize()
        {
            return JsonUtility.ToJson(this);
        }
    }
}