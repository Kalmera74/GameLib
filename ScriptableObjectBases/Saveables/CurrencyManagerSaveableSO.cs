using System.Collections;
using UnityEngine;

namespace GameLib.ScriptableObjectBases.Saveables
{
    [CreateAssetMenu(menuName = "Mobiversite/SaveManager/Saveable/CurrencySaveable", fileName = "Default_Currency_Saveable")]
    public class CurrencyManagerSaveableSO : SaveableSO
    {
        public int CurrencyAmount = 0;       
       
    }
}