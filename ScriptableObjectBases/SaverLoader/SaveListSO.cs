using GameLib.Managers.SaveManager;
using System.Collections;
using UnityEngine;

namespace GameLib.ScriptableObjectBases.SaverLoader
{
    public class SaveListSO : ScriptableObject
    {
        [SerializeField] private SaveableSO[] SaveObjects;

        public SaveableSO[] GetSaveables()
        {
            return SaveObjects;
        }
        
      
    }
}