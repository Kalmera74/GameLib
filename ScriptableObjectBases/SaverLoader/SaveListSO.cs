using GameLib.Managers.SaveManager;
using System.Collections;
using UnityEngine;

namespace GameLib.ScriptableObjectBases.SaverLoader
{
    [CreateAssetMenu(menuName = "Mobiversite/SaveManager/List/Default", fileName = "Default_Save_List")]
    public class SaveListSO : ScriptableObject
    {
        [SerializeField] private SaveableSO[] SaveObjects;

        public SaveableSO[] GetSaveables()
        {
            return SaveObjects;
        }
        
      
    }
}