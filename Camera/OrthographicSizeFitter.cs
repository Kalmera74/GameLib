using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameLib.Camera
{
    /// <summary>
    /// A class that adjusts the size of an orthographic camera to fit a set of colliders or a bounding collider.
    /// </summary>
    public class OrthographicSizeFitter : MonoBehaviour
    {
        /// <summary>
        /// The main camera that will be adjusted.
        /// </summary>
        [SerializeField] private UnityEngine.Camera MainCamera;
        /// <summary>
        /// A flag indicating whether to use the bounding collider for fitting the camera.
        /// </summary>
        [SerializeField] private bool UseBoundCollider = true;
        /// <summary>
        /// The padding to add around the bounds of the colliders when fitting the camera.
        ///       /// </summary>
        [SerializeField] private float Padding = .5f;
        /// <summary>
        /// The bounding collider used for fitting the camera, if <see cref="UseBoundCollider"/> is true.
        /// </summary>
        [SerializeField] private Collider2D BoundCollider;
        /// <summary>
        /// The list of colliders used for fitting the camera, if <see cref="UseBoundCollider"/> is false.
        /// </summary>
        [SerializeField] private List<Collider2D> Colliders;
        void Start()
        {
            if (MainCamera is null)
            {
                MainCamera = UnityEngine.Camera.main;
            }
            FitCameraToBounds();
        }
        /// <summary>
        /// Adjusts the size of the camera to fit the colliders or bounding collider.
        /// </summary>
        private void FitCameraToBounds()
        {
            var bounds = CalculateBounds();
            var size = CalculateSize(bounds);
            var center = CalculateCenter(bounds);

            MainCamera.transform.position = center;
            MainCamera.orthographic = true;
            MainCamera.orthographicSize = size;

        }
        /// <summary>
        /// Calculates the size of the camera based on the bounds of the colliders or bounding collider.
        /// </summary>
        /// <param name="bounds">The bounds of the colliders or bounding collider.</param>
        /// <returns>The size of the camera.</returns>
        private float CalculateSize(Bounds bounds)
        {
            var vertical = bounds.size.y;
            var horizontal = bounds.size.x * MainCamera.pixelHeight / MainCamera.pixelWidth;

            var size = Mathf.Max(horizontal, vertical) * .5f;
            return size;

        }
        /// <summary>
        /// Calculates the center position of the camera based on the bounds of the colliders or bounding collider.
        /// </summary>
        /// <param name="bounds">The bounds of the colliders or bounding collider.</param>
        private Vector3 CalculateCenter(Bounds bounds)
        {
            var center = bounds.center + new Vector3(0, 0, -10);
            return center;
        }

        /// <summary>
        /// Calculates the bounds of the colliders or bounding collider.
        /// </summary>
        /// <returns>The bounds of the colliders or bounding collider.</returns>
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
