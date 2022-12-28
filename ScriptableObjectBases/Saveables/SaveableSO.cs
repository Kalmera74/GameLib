using System.Collections;
using UnityEngine;

namespace GameLib.ScriptableObjectBases.Saveables
{
    public abstract class SaveableSO : ScriptableObject
    {
        public string Serialize()
        {
            return JsonUtility.ToJson(this);
        }
        public void Deserialize(string data)
        {
            JsonUtility.FromJsonOverwrite(data, this);
        }
    }
}