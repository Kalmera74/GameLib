using Object = UnityEngine.Object;
using GameLib.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mobiversite
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
