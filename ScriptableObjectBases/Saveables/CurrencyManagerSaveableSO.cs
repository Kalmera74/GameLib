using System.Collections;
using UnityEngine;

namespace GameLib.ScriptableObjectBases.Saveables
{
    [CreateAssetMenu(menuName = "Mobiversite/SaveManager/Saveable/CurrencySaveable", fileName = "Currency_Save_Data")]
    public class CurrencyManagerSaveableSO : SaveableSO
    {
        public int CurrencyAmount = 0;

    }
}