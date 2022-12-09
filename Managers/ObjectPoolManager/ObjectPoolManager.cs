using UnityEngine;
namespace Mobiversite
{

    public class ObjectPoolManager : MonoBehaviour
    {
        [SerializeField] private ObjectPoolerSO<GameObject> ObjectPooler;


        void Awake()
        {
            ObjectPooler.Init();
        }

    }
}