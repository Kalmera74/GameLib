using UnityEngine;
using UnityEngine.Purchasing;

namespace GameLib.ScriptableObjectBases.InAppPurchase
{
    [CreateAssetMenu(menuName ="Mobiversite/IAP",fileName ="Product")]

    // * This should have all the data fields of the product object from UnityEngine.Purchase
    public class ProductSO : ScriptableObject
    {
        public string ProductId;
        public ProductType ProductType;
    }
}
