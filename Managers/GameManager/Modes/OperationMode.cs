using GameLib.Managers.GameManager.States;
using UnityEngine;

namespace GameLib.Managers.GameManager.Modes
{
    /// <summary>
    /// An abstract class that represents a mode of operation for a game.
    /// </summary>
    public abstract class OperationMode : MonoBehaviour
    {
        /// <summary>
        /// Operate the game in the specified state.
        /// </summary>
        /// <param name="state">The game state to operate in.</param>
        public abstract void Operate(GameState state);
    }
}
