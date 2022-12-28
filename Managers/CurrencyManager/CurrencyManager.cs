using GameLib.ScriptableObjectBases.EventDelegates;
using GameLib.ScriptableObjectBases.PrimitiveReferences;
using GameLib.ScriptableObjectBases.Saveables;
using UnityEngine;

namespace GameLib.Managers.CurrencyManager
{
    /// <summary>
    /// A class that manages the currency of a game, including gaining and spending currency, and saving and loading the currency amount.
    /// </summary>
    public class CurrencyManager : MonoBehaviour
    {
        /// <summary>
        /// The reference to the current currency amount.
        /// </summary>
        [SerializeField] private PrimitiveRefSO<int> CurrencyAmount;

        /// <summary>
        /// The event delegate to be invoked when currency is spent.
        /// </summary>
        [SerializeField] private IntEventDelegateSO OnCurrencySpendEvent;

        /// <summary>
        /// The event delegate to be invoked when currency is gained.
        /// </summary>
        [SerializeField] private IntEventDelegateSO OnCurrencyGainedEvent;

        /// <summary>
        /// The event delegate that can be used to request the gain of currency.
        /// </summary>
        [SerializeField] private IntEventDelegateSO GainCurrencyRequest;

        /// <summary>
        /// The event delegate that can be used to request the spend of currency.
        /// </summary>
        [SerializeField] private IntEventDelegateSO SpendCurrencyRequest;

        /// <summary>
        /// The saveable data for the currency amount.
        /// </summary>
        [SerializeField] private CurrencyManagerSaveableSO CurrencyData;

        /// <summary>
        /// The event delegate that can be used to request a save of the currency amount.
        /// </summary>
        [SerializeField] private VoidEventDelegateSO SaveRequestDelegate;

        private void Start()
        {
            CurrencyAmount.SetValue(CurrencyData.CurrencyAmount);

            GainCurrencyRequest.Subscribe(GainCurrency);
            SpendCurrencyRequest.Subscribe(SpendCurrency);
        }

        /// <summary>
        /// Spend a specified amount of currency.
        /// </summary>
        /// <param name="amount">The amount of currency to spend.</param>
        public void SpendCurrency(int amount)
        {
            int currentAmount = CurrencyAmount.GetValue();
            if (CurrencyAmount.GetValue() >= amount)
            {
                CurrencyAmount.SetValue(CurrencyAmount.GetValue() - amount);
                OnCurrencySpendEvent.FireEvent(amount);
                Save();
            }
        }

        /// <summary>
        /// Gain a specified amount of currency.
        /// </summary>
        /// <param name="amount">The amount of currency to gain.</param>
        public void GainCurrency(int amount)
        {
            int currentAmount = CurrencyAmount.GetValue();
            CurrencyAmount.SetValue(currentAmount + amount);
            OnCurrencyGainedEvent.FireEvent(amount);
            Save();
        }

        /// <summary>
        /// Save the current currency amount.
        /// </summary>
        private void Save()
        {
            CurrencyData.CurrencyAmount = CurrencyAmount.GetValue();
            SaveRequestDelegate?.FireEvent();
        }
    }
}
