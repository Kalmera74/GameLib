using GameLib.Managers.SaveManager;
using System.Collections;
using System.Text.Json.Nodes;
using UnityEngine;

namespace GameLib.ScriptableObjectBases.SaverLoader
{
    public class LoaderSO : ScriptableObject, ILoader
    {
        [SerializeField] private SaveListSO SaveList;
        public void Load()
        {
            // ! Get the saveables and desialize the json to the each SO in the order they are in the list
            var saveables = SaveList.GetSaveables();

            string savedData = PlayerPrefs.GetString("saveData");

            JsonArray jsonList = JsonValue.Parse(savedData) as JsonArray;

            int length = jsonList.Count;

            for (int i = 0; i < length; i++)
            {
                var objectStr = jsonList[i];
                SaveableSO saveable = saveables[i];
                saveable.Deserialize(objectStr.ToString());
            }
        }
    }
}