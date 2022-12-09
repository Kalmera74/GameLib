using GameLib.Managers.GameManager.States;
using UnityEngine;

namespace GameLib.Managers.GameManager.Modes
{
    public class UpdateMode : OperationMode
    {
        private GameState _state;
        public override void Operate(GameState state)
        {
            _state = state;
        }

        void Update()
        {
            if (_state is null)
            {
                return;
            }

            _state.Execute();
        }
    }
}
