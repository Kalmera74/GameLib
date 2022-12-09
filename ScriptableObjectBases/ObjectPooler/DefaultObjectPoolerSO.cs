using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameLib.ScriptableObjectBases.ObjectPooler
{

    [CreateAssetMenu(menuName = "Mobiversite/Data Containers/ObjectPooling", fileName = "Object_Pool_Container")]
    public class DefaultObjectPoolerSO : ObjectPoolerSO<GameObject>
    {
        public override void Init()
        {

            foreach (var item in Pool)
            {
                int count = item.AmountToSpawn;
                GameObject prefab = item.Object;

                Queue<GameObject> gameObjectQueue = new Queue<GameObject>();

                for (int i = 0; i < count; i++)
                {
                    GameObject obj = Instantiate(prefab, Vector3.zero, Quaternion.identity);

                    obj.SetActive(false);
                    gameObjectQueue.Enqueue(obj);
                }

                PooledObjectData pooledData = new PooledObjectData
                {
                    ObjectQueue = gameObjectQueue,
                    InstantiateOnDemand = item.InstantiateOnDemand,
                    CanBeUsedBeforeReleased = item.CanBeUsedBeforeReleased
                };

                int key = item.Object.GetInstanceID();
                _pool.Add(key, pooledData);

            }
        }

        public override GameObject Spawn(GameObject objectToClone, Vector3 spawnPosition, Quaternion spawnRotation, bool isActive = true)
        {
            if (_pool.TryGetValue(objectToClone.GetInstanceID(), out PooledObjectData pooledData))
            {
                var queue = pooledData.ObjectQueue;

                if (queue.Count < 1)
                {
                    if (!pooledData.InstantiateOnDemand)
                    {

                        return null;
                    }

                    return Instantiate(objectToClone, spawnPosition, spawnRotation);
                }

                GameObject obj = queue.Dequeue();

                obj.transform.localPosition = spawnPosition;
                obj.transform.localRotation = spawnRotation;
                obj.SetActive(isActive);

                if (pooledData.CanBeUsedBeforeReleased)
                {
                    queue.Enqueue(obj);
                }

                return obj;

            }
            return null;
        }

        public override void Destroy(GameObject obj)
        {
            obj.SetActive(false);
            int key = obj.GetInstanceID();
            _pool[key].ObjectQueue.Enqueue(obj);
        }
    }
}
