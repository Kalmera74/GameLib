using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;
namespace Mobiversite.GameLib.DevLib.UI
{
    public class MainCanvasManager : MonoBehaviour
    {
        public static MainCanvasManager Instance;
        [SerializeField] private List<Object> UIManagers = new List<Object>();


        public Object Get(Type type)
        {
            foreach (var manager in UIManagers)
            {
                if (manager.GetType().Equals(type))
                {
                    return manager;
                }
            }
            return null;
        }

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
