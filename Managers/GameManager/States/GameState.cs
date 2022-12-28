using UnityEngine;

namespace GameLib.Managers.GameManager.States
{
    /// <summary>
    /// An abstract class that represents a state in a game.
    /// </summary>
    public abstract class GameState : MonoBehaviour
    {
        /// <summary>
        /// Execute the logic for this game state.
        /// </summary>
        public abstract void Execute();
    }
}
