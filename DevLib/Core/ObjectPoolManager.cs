using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mobiversite.GameLib.DevLib.Core
{
    [Serializable]
    public struct PoolData
    {
        public GameObject Object;
        public int Count;
        public bool InstantiateOnDemand;
    }
    public struct PooledObjectData
    {
        public List<GameObject> Objects;
        public bool InstantiateOnDemand;
    }
    public class ObjectPoolManager : MonoBehaviour
    {
        [SerializeField] private List<PoolData> ObjectPool = new List<PoolData>();

        private Dictionary<int, PooledObjectData> _pool = new Dictionary<int, PooledObjectData>();

        public static ObjectPoolManager Instance;


        private void Awake()
        {
            FillList();
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
        void Start()
        {
            SceneManager.Instance.OnBeforeSceneLoaded += ReclaimObjects;
        }

        private void ReclaimObjects()
        {
            foreach (var item in _pool)
            {
                var items = item.Value.Objects;
                foreach (var obj in items)
                {
                    Destroy(obj);
                }
            }
        }

        private void FillList()
        {
            foreach (var item in ObjectPool)
            {

                int count = item.Count;
                var list = new List<GameObject>();
                for (int i = 0; i < count; i++)
                {
                    GameObject obj = Instantiate(item.Object, Vector3.zero, Quaternion.identity);
                    obj.transform.SetParent(transform);
                    obj.SetActive(false);
                    list.Add(obj);
                }
                var data = new PooledObjectData
                {
                    Objects = list,
                    InstantiateOnDemand = item.InstantiateOnDemand
                };
                var key = item.Object.GetInstanceID();
                if (_pool.ContainsKey(key))
                {

                    var objData = _pool[key];
                    list.ForEach(e => objData.Objects.Add(e));
                }
                else
                {
                    _pool.Add(item.Object.GetInstanceID(), data);
                }
            }

        }
        public void Destroy(GameObject obj)
        {
            obj?.transform.SetParent(transform);
            obj?.SetActive(false);
        }
        public GameObject Spawn(GameObject objectToClone, bool isActive = true)
        {

            return Spawn(objectToClone, Vector3.zero, Quaternion.identity, isActive);
        }
        public GameObject Spawn(GameObject objectToClone, Vector3 spawnPosition, Quaternion rotation, bool isActive = true)
        {
            if (_pool.TryGetValue(objectToClone.GetInstanceID(), out PooledObjectData list))
            {
                foreach (var item in list.Objects)
                {
                    if (!item.activeInHierarchy)
                    {
                        var obj = item;
                        obj.transform.position = spawnPosition;
                        obj.transform.rotation = rotation;
                        obj.SetActive(isActive);
                        return obj;
                    }
                }

                if (list.InstantiateOnDemand)
                {
                    return Instantiate(objectToClone, spawnPosition, rotation);
                }
            }
            return null;
        }
        void OnDestroy()
        {
            SceneManager.Instance.OnBeforeSceneLoaded -= ReclaimObjects;
        }
    }
}
