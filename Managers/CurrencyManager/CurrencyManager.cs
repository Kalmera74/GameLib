using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Mobiversite
{
    public class CurrencyManager : MonoBehaviour
    {
        [SerializeField] private PrimitiveRefSO<int> CurrencyAmount;
        [SerializeField] private IntEventDelegateSO OnCurrencySpendEvent;
        [SerializeField] private IntEventDelegateSO OnCurrencyGainedEvent;

        [SerializeField] private IntEventDelegateSO GainCurrencyRequest;
        [SerializeField] private IntEventDelegateSO SpendCurrencyRequest;
        void Start()
        {
            // ! Get and set the current amount from the save system

            GainCurrencyRequest.Subscribe(GainCurrency);
            SpendCurrencyRequest.Subscribe(SpendCurrency);
        }


        public void SpendCurrency(int amount)
        {
            int currentAmount = CurrencyAmount.GetValue();
            if (CurrencyAmount.GetValue() >= amount)
            {
                CurrencyAmount.SetValue(CurrencyAmount.GetValue() - amount);
            }
            OnCurrencySpendEvent.FireEvent(amount);
        }

        public void GainCurrency(int amount)
        {
            int currentAmount = CurrencyAmount.GetValue();
            CurrencyAmount.SetValue(currentAmount + amount);
            OnCurrencyGainedEvent.FireEvent(amount);
        }


    }
}
