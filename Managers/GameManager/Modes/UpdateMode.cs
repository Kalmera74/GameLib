using Mobiversite.GameLib.DevLib.Core;
using UnityEngine;

namespace Mobiversite
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
