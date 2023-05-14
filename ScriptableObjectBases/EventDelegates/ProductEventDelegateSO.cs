using GameLib.ScriptableObjectBases.InAppPurchase;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameLib.ScriptableObjectBases.EventDelegates
{
    [CreateAssetMenu(menuName = "Game/EventDelegates/Product Param Event Delegate", fileName = "Product_Delegate")]
    public class ProductEventDelegateSO : EventDelegateSO<ProductSO>
    {
    }
}
