using GameLib.Managers.SaveManager;
using System.Collections;
using UnityEngine;

namespace GameLib.ScriptableObjectBases.SaverLoader
{
    [CreateAssetMenu(menuName = "Mobiversite/SaveManager/Saver/Default", fileName = "Default_Saver")]
    public class SaverSO : ScriptableObject, ISaver
    {
        [SerializeField] private SaveListSO SaveList;
        public void Save()
        {
            var saveables = SaveList.GetSaveables();
            var length = saveables.Length;
            const string jsonHead = "[";
            const string jsonTail = "]";
            string json = jsonHead;


            for (int i = 0; i < length; i++)
            {
                SaveableSO saveable = saveables[i];

                string serializedSaveableSO = $"{saveable.Serialize()},";
                json += serializedSaveableSO;
            }

            json += jsonTail;
            Debug.Log($"!!! {json}");
            PlayerPrefs.SetString("saveData", json);
            PlayerPrefs.Save();

        }
    }
}