using GameLib.ScriptableObjectBases.ObjectPooler;
using UnityEngine;
namespace GameLib.Managers.ObjectPoolManager
{

    /// <summary>
    /// Manages the object pool of game objects.
    /// </summary>
    public class ObjectPoolManager : MonoBehaviour
    {
        /// <summary>
        /// The object pooler for the game objects.
        /// </summary>
        [SerializeField] private ObjectPoolerSO<GameObject> ObjectPooler;

        /// <summary>
        /// Initializes the object pooler.
        /// </summary>
        void Awake()
        {
            ObjectPooler.Init();
        }
    }

}