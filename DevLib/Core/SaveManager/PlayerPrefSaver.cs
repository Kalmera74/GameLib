using UnityEngine;

namespace Mobiversite.GameLib.DevLib.Core
{
    public class PlayerPrefSaver : MonoBehaviour, ISaver
    {
        [SerializeField] private const string _saveKey = "dataObjectSaveKey";
        public bool Save(SaveDataObject data)
        {
            string serializedData = JsonUtility.ToJson(data);
            PlayerPrefs.SetString(_saveKey, serializedData);
            PlayerPrefs.Save();
            return true;
        }
    }
}
