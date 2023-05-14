using System;

namespace Game
{
    public class GoogleValidationRequestData
    {
        public string orderId;
        public string packageName;
        public string productId;
        public long purchaseTime;
        public int purchaseState;
        public string purchaseToken;
        public int quantity;
        public bool acknowledged;
        public string deviceId;
    }
}
