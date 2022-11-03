using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mobiversite.GameLib.DevLib.UI
{
    public class MainCanvasManager : MonoBehaviour
    {
        public static MainCanvasManager Instance;

        void Awake()
        {
            if (Instance is null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                DestroyImmediate(gameObject);
            }
        }
    }
}
