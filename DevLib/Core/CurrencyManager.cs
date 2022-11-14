using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Mobiversite.GameLib.DevLib.Core
{
    public class CurrencyManager : MonoBehaviour
    {
        [SerializeField] private int CurrencyAmount = 0;
        public static CurrencyManager Instance;
        public event Action<int> OnCurrencySpend;
        public event Action<int> OnCurrencyGained;
        public event Action OnCurrencyAmountChanged;

        void Awake()
        {
            if (Instance is null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                DestroyImmediate(gameObject);
            }
            CurrencyAmount = SaveManager.Instance.GetCurrencyAmount();
        }
        void Start()
        {


        }

        public int GetCurrentAmountOfCurrency()
        {
            return CurrencyAmount;
        }
        public void SpendCurrency(int amount)
        {
            if (CurrencyAmount - amount < 0)
            {
                return;
            }
            CurrencyAmount -= amount;
            OnCurrencySpend?.Invoke(amount);
            OnCurrencyAmountChanged?.Invoke();
            Save();
        }

        public void GainCoin(int amount)
        {
            CurrencyAmount += amount;
            OnCurrencyGained?.Invoke(amount);
            OnCurrencyAmountChanged?.Invoke();
            Save();
        }

        private void Save()
        {
            SaveManager.Instance.SaveCurrencyAmount(CurrencyAmount);
        }
    }
}
