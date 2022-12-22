using System.Collections;
using UnityEngine;

namespace GameLib.ScriptableObjectBases.SaverLoader
{
    public abstract class SaveableSO : ScriptableObject
    {
        public abstract string Serialize();
        public abstract void Deserialize(string data);
    }
}