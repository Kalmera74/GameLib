using Mobiversite.GameLib.DevLib.Core.GameManagerBase.States;

namespace Mobiversite.GameLib.DevLib.Core.GameManagerBase.Modes
{
    public interface IOperationMode
    {
        public void Operate(IGameState state);
    }
}
