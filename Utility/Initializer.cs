using Object = UnityEngine.Object;
using UnityEngine;

namespace GameLib.Utility
{
    public class Initializer : MonoBehaviour
    {
        public List<Object> Initializables;

        private void Awake()
        {
            foreach (var obj in Initializables)
            {
                if (obj is IInitializable)
                {
                    IInitializable initializable = (IInitializable)obj;

                    initializable.Init();
                }
            }
        }
    }
}
