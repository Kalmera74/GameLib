using GameLib.Managers.SaveManager;
using UnityEngine;
using Newtonsoft.Json.Linq;
using GameLib.ScriptableObjectBases.Saveables;

namespace GameLib.ScriptableObjectBases.SaverLoader
{
    /// <summary>
    /// ScriptableObject class that represents a loader for loading saved data.
    /// </summary>
    [CreateAssetMenu(menuName = "Game/SaveManager/Loader/Default", fileName = "Default_Loader")]
    public class LoaderSO : ScriptableObject, ILoader
    {
        /// <summary>
        /// The list of saveables to load.
        /// </summary>
        [SerializeField] private SaveListSO SaveList;

        /// <summary>
        /// Loads the saved data.
        /// </summary>
        public void Load()
        {
            // Get the list of saveables
            var saveables = SaveList.GetSaveables();

            // Get the saved data from PlayerPrefs
            string savedData = PlayerPrefs.GetString("saveData");

            // If there is no saved data, return
            if (string.IsNullOrEmpty(savedData))
            {
                return;
            }

            // Parse the saved data as a JSON array
            var jsonList = JArray.Parse(savedData);

            // Get the length of the JSON array
            int length = jsonList.Count;

            // Loop through the array and deserialize each saveable
            for (int i = 0; i < length; i++)
            {
                // Get the serialized saveable as a JSON string
                var objectStr = jsonList[i];
                // Get the saveable to deserialize
                SaveableSO saveable = saveables[i];
                // Deserialize the saveable
                saveable.Deserialize(objectStr.ToString());
            }
        }
    }
}
