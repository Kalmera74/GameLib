using System.Collections;
using UnityEngine;

namespace GameLib.ScriptableObjectBases.Saveables
{
    /// <summary>
    /// Abstract base class for ScriptableObjects that can be serialized and deserialized.
    /// </summary>
    public abstract class SaveableSO : ScriptableObject
    {
        /// <summary>
        /// Serializes the object to a JSON string.
        /// </summary>
        /// <returns>The serialized JSON string.</returns>
        public string Serialize()
        {
            return JsonUtility.ToJson(this);
        }

        /// <summary>
        /// Deserializes the object from a JSON string.
        /// </summary>
        /// <param name="data">The JSON string to deserialize.</param>
        public void Deserialize(string data)
        {
            JsonUtility.FromJsonOverwrite(data, this);
        }
    }
}
