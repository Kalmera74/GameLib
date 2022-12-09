using UnityEngine;

namespace GameLib.Managers.GameManager.States
{
    public abstract class GameState : MonoBehaviour
    {
        public abstract void Execute();
    }
}
