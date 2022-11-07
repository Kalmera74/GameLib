using UnityEngine;

namespace Mobiversite.GameLib.DevLib.Core.SaveSystem
{
    public class PlayerPrefLoader : MonoBehaviour, ILoader
    {
        [SerializeField] private const string _saveKey = "dataObjectSaveKey";

        public SaveDataObject Load()
        {
            var result = PlayerPrefs.GetString(_saveKey, string.Empty);
            if (result.Equals(string.Empty))
            {
                return null;
            }

            var deserializedData = JsonUtility.FromJson<SaveDataObject>(result);
            return deserializedData;

        }
    }
}
