using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mobiversite
{
    [Serializable]
    public class PrimitiveRefSO<T> : ScriptableObject
    {
        [SerializeField] private T ReferenceValue;
        public void SetValue(T value)
        {
            ReferenceValue = value;
        }
        public T GetValue()
        {
            return ReferenceValue;
        }
    }
}