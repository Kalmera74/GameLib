using GameLib.Managers.SaveManager;
using GameLib.ScriptableObjectBases.Saveables;
using UnityEngine;

namespace GameLib.ScriptableObjectBases.SaverLoader
{
    /// <summary>
    /// ScriptableObject class that represents a saver for saving game data.
    /// </summary>
    [CreateAssetMenu(menuName = "Mobiversite/SaveManager/Saver/Default", fileName = "Default_Saver")]
    public class SaverSO : ScriptableObject, ISaver
    {
        /// <summary>
        /// The list of saveables to save.
        /// </summary>
        [SerializeField] private SaveListSO SaveList;

        /// <summary>
        /// Saves the game data.
        /// </summary>
        public void Save()
        {
            // Get the list of saveables
            var saveables = SaveList.GetSaveables();
            // Get the length of the saveables list
            var length = saveables.Length;
            // Head and tail of the JSON array
            const string jsonHead = "[";
            const string jsonTail = "]";
            // Initialize the JSON string
            string json = jsonHead;

            // Loop through the saveables and serialize each one
            for (int i = 0; i < length; i++)
            {
                // Get the saveable to serialize
                SaveableSO saveable = saveables[i];
                // Serialize the saveable
                string serializedSaveableSO = $"{saveable.Serialize()},";
                // Append the serialized saveable to the JSON string
                json += serializedSaveableSO;
            }

            // Add the tail to the JSON string
            json += jsonTail;
            // Save the JSON string to PlayerPrefs
            PlayerPrefs.SetString("saveData", json);
            // Save the PlayerPrefs
            PlayerPrefs.Save();
        }
    }
}
