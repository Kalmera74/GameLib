using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameLib.Camera
{
    public class OrthographicSizeFitter : MonoBehaviour
    {
        [SerializeField] private UnityEngine.Camera MainCamera;
        [SerializeField] private bool UseBoundCollider = true;
        [SerializeField] private float Padding = .5f;
        [SerializeField] private Collider2D BoundCollider;
        [SerializeField] private List<Collider2D> Colliders = new List<Collider2D>();
        void Start()
        {
            if (MainCamera is null)
            {
                MainCamera = UnityEngine.Camera.main;
            }
            FitCameraToBounds();
        }
        private void FitCameraToBounds()
        {
            var bounds = CalculateBounds();
            var size = CalculateSize(bounds);
            var center = CalculateCenter(bounds);

            MainCamera.transform.position = center;
            MainCamera.orthographic = true;
            MainCamera.orthographicSize = size;

        }

        private float CalculateSize(Bounds bounds)
        {
            var vertical = bounds.size.y;
            var horizontal = bounds.size.x * MainCamera.pixelHeight / MainCamera.pixelWidth;

            var size = Mathf.Max(horizontal, vertical) * .5f;
            return size;

        }
        private Vector3 CalculateCenter(Bounds bounds)
        {
            var center = bounds.center + new Vector3(0, 0, -10);
            return center;
        }


        private Bounds CalculateBounds()
        {
            Bounds bound;

            if (UseBoundCollider)
            {
                bound = BoundCollider.bounds;
            }
            else
            {

                bound = new Bounds();

                foreach (var collider in Colliders)
                {
                    bound.Encapsulate(collider.bounds);
                }
            }

            bound.Expand(Padding);
            return bound;
        }
    }
}
