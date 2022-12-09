

using UnityEngine;

namespace Mobiversite
{
    public abstract class OperationMode : MonoBehaviour
    {
        public abstract void Operate(GameState state);
    }
}
