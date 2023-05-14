using GameLib.ScriptableObjectBases.EventDelegates;
using GameLib.ScriptableObjectBases.InAppPurchase;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Purchasing;

namespace GameLib.InAppPurchase
{
    public class InAppPurchase : MonoBehaviour, IStoreListener
    {
        [SerializeField] private string ValidaitonServerAddress;
        [SerializeField] private string IOSEndPoint;
        [SerializeField] private string AndroidEndPoint;

        [SerializeField] private VoidEventDelegateSO OnBeforePurchaseBeginEventDelegate;
        [SerializeField] private VoidEventDelegateSO OnPurchaseSuccedEventDelegate;
        [SerializeField] private VoidEventDelegateSO OnPurchaseFailedEventDelegate;
        [SerializeField] private VoidEventDelegateSO OnAfterPurchaseDoneEventDelegate;

        [SerializeField] private ProductEventDelegateSO BuyProductRequestDelegate;
     
        [SerializeField] private ProductListSO ProducList;

        private IStoreController _storeController;
        private UnityWebRequest _socket;

        void Awake()
        {
            BuyProductRequestDelegate.Subscribe(Buy);
        }

        private void InitilazieProducts()
        {

        }

        private void SendValidaitonRequest(string data)
        {

        }



        private void Buy(ProductSO product)
        {
            _storeController?.InitiatePurchase(product.ProductId);
        }

        void InitializePurchasing()
        {
          
        }

        public void OnInitializeFailed(InitializationFailureReason error)
        {

        }

        public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
        {

         

            return PurchaseProcessingResult.Complete;
        }

        public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
        {


        }

        public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
        {
            
        }

    }
}
