using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameLib.ScriptableObjectBases.ObjectPooler
{
    /// <summary>
    /// ScriptableObject class that represents an object pooler for GameObjects.
    /// </summary>
    [CreateAssetMenu(menuName = "Mobiversite/Data Containers/ObjectPooling", fileName = "Object_Pool_Container")]
    public class ObjectPoolerSO : ScriptableObject
    {
        /// <summary>
        /// Struct that represents data for a single object in the object pool.
        /// </summary>
        [Serializable]
        protected struct PoolData
        {
            /// <summary>
            /// The object to be pooled.
            /// </summary>
            public GameObject Object;
            /// <summary>
            /// The number of instances of the object to be spawned when the object pool is initialized.
            /// </summary>
            public int AmountToSpawn;
            /// <summary>
            /// Indicates whether new instances of the object should be instantiated on demand when the object pool is empty.
            /// </summary>
            public bool InstantiateOnDemand;
            /// <summary>
            /// Indicates whether the object can be used before it is released back to the object pool.
            /// </summary>
            public bool CanBeUsedBeforeReleased;
        }

        /// <summary>
        /// Struct that represents data for a single pooled object.
        /// </summary>
        protected struct PooledObjectData
        {
            /// <summary>
            /// Queue of objects available for use in the object pool.
            /// </summary>
            public Queue<GameObject> ObjectQueue;
            /// <summary>
            /// Indicates whether new instances of the object should be instantiated on demand when the object pool is empty.
            /// </summary>
            public bool InstantiateOnDemand;
            /// <summary>
            /// Indicates whether the object can be used before it is released back to the object pool.
            /// </summary>
            public bool CanBeUsedBeforeReleased;
        }

        /// <summary>
        /// List of data for the objects in the object pool.
        /// </summary>
        [SerializeField] private List<PoolData> Pool = new List<PoolData>();
        /// <summary>
        /// Dictionary of data for the pooled objects, keyed by the object's instance ID.
        /// </summary>
        private Dictionary<int, PooledObjectData> _pool = new Dictionary<int, PooledObjectData>();

        /// <summary>
        /// Initializes the object pool.
        /// </summary>
        public void Init()
        {
            // Iterate through the list of objects to be pooled
            foreach (var item in Pool)
            {
                // Get the number of instances to spawn and the prefab to use for spawning
                int count = item.AmountToSpawn;
                GameObject prefab = item.Object;

                // Create a queue of GameObjects for the pooled object
                Queue<GameObject> gameObjectQueue = new Queue<GameObject>();

                // Instantiate the specified number of objects and add them to the queue
                for (int i = 0; i < count; i++)
                {
                    GameObject obj = Instantiate(prefab, Vector3.zero, Quaternion.identity);
                    // Set the object to inactive
                    obj.SetActive(false);
                    // Add the object to the queue
                    gameObjectQueue.Enqueue(obj);
                }

                // Create a PooledObjectData struct for the pooled object
                PooledObjectData pooledData = new PooledObjectData
                {
                    ObjectQueue = gameObjectQueue,
                    InstantiateOnDemand = item.InstantiateOnDemand,
                    CanBeUsedBeforeReleased = item.CanBeUsedBeforeReleased
                };

                // Add the pooled object data to the dictionary, using the object's instance ID as the key
                int key = item.Object.GetInstanceID();
                _pool.Add(key, pooledData);
            }
        }
        /// <summary>
        /// Spawns an instance of the given object from the object pool.
        /// </summary>
        /// <param name="objectToClone">The object to spawn.</param>
        /// <param name="spawnPosition">The position at which to spawn the object.</param>
        /// <param name="spawnRotation">The rotation of the spawned object.</param>
        /// <param name="isActive">Indicates whether the spawned object should be active or inactive.</param>
        /// <returns>The spawned object.</returns>
        public GameObject Spawn(GameObject objectToClone, Vector3 spawnPosition, Quaternion spawnRotation, bool isActive = true)
        {
            // Try to get the pooled object data for the object to be spawned
            if (_pool.TryGetValue(objectToClone.GetInstanceID(), out PooledObjectData pooledData))
            {
                // Get the queue of available objects for the pooled object
                var queue = pooledData.ObjectQueue;
                // If the queue is empty
                if (queue.Count < 1)
                {
                    // If new objects should not be instantiated on demand, return null
                    if (!pooledData.InstantiateOnDemand)
                    {

                        return null;
                    }
                    // If new objects should be instantiated on demand, instantiate it and add it to the queue so that queue size increased for feature needs
                    // Then return the cloned object
                    GameObject clonedObject = Instantiate(objectToClone, spawnPosition, spawnRotation);
                    clonedObject.SetActive(isActive);
                    queue.Enqueue(clonedObject);
                    return clonedObject;
                }
                // Dequeue an available object from the queue
                GameObject obj = queue.Dequeue();

                // Set the object's position and rotation
                obj.transform.localPosition = spawnPosition;
                obj.transform.localRotation = spawnRotation;
                // Set the object to active or inactive as specified
                obj.SetActive(isActive);
                // If the object can be used before it is released back to the object pool, add it back to the queue
                if (pooledData.CanBeUsedBeforeReleased)
                {
                    queue.Enqueue(obj);
                }

                // Return the object
                return obj;
            }

            // If the object to be spawned was not found in the object pool, return null
            return null;
        }

        /// <summary>
        /// Releases the given object back to the object pool.
        /// </summary>
        /// <param name="obj">The object to release.</param>
        public void Destroy(GameObject obj)
        {
            // Set the object to inactive
            obj.SetActive(false);
            // Get the object's instance ID
            int key = obj.GetInstanceID();
            // Add the object to the queue for its respective pooled object
            _pool[key].ObjectQueue.Enqueue(obj);
        }
    }
}