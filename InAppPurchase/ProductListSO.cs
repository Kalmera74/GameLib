using GameLib.ScriptableObjectBases.InAppPurchase;
using UnityEngine;

namespace GameLib.InAppPurchase
{
    [CreateAssetMenu(menuName ="Game/IAP",fileName ="ProductList")]
    public class ProductListSO : ScriptableObject
    {
        public List<ProductSO> Products;
    }
}
