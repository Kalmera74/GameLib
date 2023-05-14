using UnityEngine;
namespace GameLib.Camera
{

    public class SafeAreaFitter : MonoBehaviour
    {
        [SerializeField] private RectTransform ParentRectTransform;

        private Rect _safeArea;
        private Vector2 _minAnchor;
        private Vector2 _maxAnchor;

        void Awake()
        {
            _safeArea = Screen.safeArea;

            _minAnchor = _safeArea.position;
            _maxAnchor = _minAnchor + _safeArea.size;

            _minAnchor.x /= Screen.width;
            _minAnchor.y /= Screen.height;

            _maxAnchor.x /= Screen.width;
            _maxAnchor.y /= Screen.height;

            ParentRectTransform.anchorMin = _minAnchor;
            ParentRectTransform.anchorMax = _maxAnchor;
        }
    }

}
