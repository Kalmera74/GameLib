using GameLib.Managers.GameManager.States;
using UnityEngine;

namespace GameLib.Managers.GameManager.Modes
{
    public abstract class OperationMode : MonoBehaviour
    {
        public abstract void Operate(GameState state);
    }
}
