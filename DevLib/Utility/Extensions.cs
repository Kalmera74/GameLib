using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mobiversite.GameLib.DevLib.Utility
{
    public static class Extensions
    {
        public static T RandomElement<T>(this List<T> list)
        {
            var randomIndex = Random.Range(0, list.Count - 1);
            return list[randomIndex];
        }
        public static Color RandomColor(this Color color)
        {
            float r = Random.Range(0f, 1f);
            float g = Random.Range(0f, 1f);
            float b = Random.Range(0f, 1f);

            return new Color(r, g, b, 1f);
        }

        public static void ResetTransform(this Transform trans)
        {
            trans.localPosition = Vector3.zero;
            trans.localScale = Vector3.one;
            trans.localRotation = Quaternion.Euler(Vector3.zero);
        }
    }
}
