using Mobiversite.GameLib.DevLib.Core;
using UnityEngine;

namespace Mobiversite.GameLib.DevLib.Core
{
    public class UpdateMode : MonoBehaviour, IOperationMode
    {
        private IGameState _state;
        public void Operate(IGameState state)
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
