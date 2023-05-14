using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GameLib.Utility
{
    /// <summary>
    /// A utility class that contains extension methods for UnityEngine classes.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Returns a random element from the given list.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the list.</typeparam>
        /// <param name="list">The list to pick a random element from.</param>
        /// <returns>A random element from the given list.</returns>
        public static T RandomElement<T>(this List<T> list)
        {
            var randomIndex = Random.Range(0, list.Count - 1);
            return list[randomIndex];
        }

        /// <summary>
        /// Returns a random color
        /// </summary>
        /// <param name="color">The input color</param>
        /// <returns>A random color</returns>
        public static Color RandomColor(this Color color)
        {
            float r = Random.Range(0f, 1f);
            float g = Random.Range(0f, 1f);
            float b = Random.Range(0f, 1f);

            return new Color(r, g, b, 1f);

        }

        /// <summary>
        /// Resets the transform's position, scale and rotation
        /// </summary>
        /// <param name="trans">The transform to be reset</param>
        public static void ResetTransform(this Transform trans)
        {
            trans.localPosition = Vector3.zero;
            trans.localScale = Vector3.one;
            trans.localRotation = Quaternion.Euler(Vector3.zero);
        }
    }
}
