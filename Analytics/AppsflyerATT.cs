using System.Collections;
using System.Collections.Generic;
using AppsFlyerSDK;
using UnityEngine;
#if UNITY_IOS
using Unity.Advertisement.IosSupport;
#endif

namespace Mobiversite
{
    public class AppsflyerATT : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            DontDestroyOnLoad(this);

            AppsFlyer.initSDK("m33mprW5rxe9K26pD3r4qR", "com.mobiversite.alchemypuzzle");
            StartCoroutine(Ask());
        }

        private void ShowATT()
        {
#if UNITY_IOS && !UNITY_EDITOR
          if(ATTrackingStatusBinding.GetAuthorizationTrackingStatus() == ATTrackingStatusBinding.AuthorizationTrackingStatus.NOT_DETERMINED)
           {
             ATTrackingStatusBinding.RequestAuthorizationTracking();
               AppsFlyer.waitForATTUserAuthorizationWithTimeoutInterval(60);
            }
#endif
        }
        private IEnumerator Ask()
        {
            yield return new WaitForSeconds(.25f);
            ShowATT();
            AppsFlyer.startSDK();
        }

    }
}
