using Mobiversite.GameLib.DevLib.Core.GameManagerBase.States;
using UnityEngine;

namespace Mobiversite.GameLib.DevLib.Core.GameManagerBase.Modes
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
