using Mobiversite.GameLib.DevLib.Core;

namespace Mobiversite.GameLib.DevLib.Core
{
    public interface IOperationMode
    {
        public void Operate(IGameState state);
    }
}
