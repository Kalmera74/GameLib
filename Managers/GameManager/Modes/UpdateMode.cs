using GameLib.Managers.GameManager.States;
using UnityEngine;

namespace GameLib.Managers.GameManager.Modes
{
    /// <summary>
    /// A class that represents an operation mode that updates the game state in the `Update` method.
    /// </summary>
    public class UpdateMode : OperationMode
    {
        /// <summary>
        /// The current game state.
        /// </summary>
        private GameState _state;

        /// <summary>
        /// Operate the game in the specified state.
        /// </summary>
        /// <param name="state">The game state to operate in.</param>
        public override void Operate(GameState state)
        {
            _state = state;
        }

        private void Update()
        {
            if (_state is null)
            {
                return;
            }

            _state.Execute();
        }
    }
}
