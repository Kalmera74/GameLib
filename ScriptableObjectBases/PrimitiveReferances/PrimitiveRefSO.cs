using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameLib.ScriptableObjectBases.PrimitiveReferences
{
    /// <summary>
    /// ScriptableObject class that represents a reference to a primitive value.
    /// </summary>
    [Serializable]
    public abstract class PrimitiveRefSO<T> : ScriptableObject
    {
        /// <summary>
        /// The reference value.
        /// </summary>
        [SerializeField] private T ReferenceValue;

        /// <summary>
        /// Sets the reference value.
        /// </summary>
        /// <param name="value">The value to set the reference to.</param>
        public void SetValue(T value)
        {
            ReferenceValue = value;
        }

        /// <summary>
        /// Gets the reference value.
        /// </summary>
        /// <returns>The reference value.</returns>
        public T GetValue()
        {
            return ReferenceValue;
        }
    }
}
