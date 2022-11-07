

using UnityEngine;

namespace Mobiversite.GameLib.DevLib.Core.SaveSystem
{
    public interface ISaver
    {
        public bool Save(SaveDataObject data);
    }
}
