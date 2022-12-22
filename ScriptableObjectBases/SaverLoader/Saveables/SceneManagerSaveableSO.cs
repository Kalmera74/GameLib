using System.Collections;
using UnityEngine;

namespace GameLib.ScriptableObjectBases.SaverLoader.Saveables
{
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