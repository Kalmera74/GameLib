using GameLib.Managers.SaveManager;
using System.Collections;
using Newtonsoft.Json;
using UnityEngine;
using Newtonsoft.Json.Linq;
using GameLib.ScriptableObjectBases.Saveables;

namespace GameLib.ScriptableObjectBases.SaverLoader
{
    [CreateAssetMenu(menuName = "Mobiversite/SaveManager/Loader/Default", fileName = "Default_Loader")]
    public class LoaderSO : ScriptableObject, ILoader
    {
        [SerializeField] private SaveListSO SaveList;
        public void Load()
        {
            // ! Get the saveables and desialize the json to the each SO in the order they are in the list
            var saveables = SaveList.GetSaveables();

            string savedData = PlayerPrefs.GetString("saveData");

            if (string.IsNullOrEmpty(savedData))
            {
                return;
            }
            //  JsonArray jsonList = JsonValue.Parse(savedData) as JsonArray;

            var jsonList = JArray.Parse(savedData);


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