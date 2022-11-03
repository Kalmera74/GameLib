
using UnityEngine;

namespace Mobiversite.GameLib.DevLib.Core.GameManagerBase.States
{
    public class InitState : MonoBehaviour, IGameState
    {
        public void Execute()
        {
            Debug.Log($"!!! Initializing Some Stuff");
        }
    }
}
