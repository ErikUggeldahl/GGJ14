using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Pool
{
    public class PoolManager : MonoBehaviour
    {
        // if stale object pools should be cleaned up periodically by destroying deactivated game objects
        public bool CleanUpStaleObjects = false;
        // if cleanUpStaleObjects is true: How many seconds an object pool needs to be stale before a cleanup is performed
        public int SecondsStaleBeforeCleanup = 500;
        private static List<ObjectPool> _pools = new List<ObjectPool>();
        private float _secondsPoolRefreshRate = 15;

        void Awake()
        {
            if (CleanUpStaleObjects)
            {
                StartCoroutine(CleanPools());
            }
        }

        private static ObjectPool CreateNewPool<T>(MonoBehaviour prefab) where T : MonoBehaviour
        {
            ObjectPool pool = new ObjectPool();
            pool.PoolType = typeof(T);
            pool.ObjectName = prefab.name;
            _pools.Add(pool);
            return pool;
        }

        private static ObjectPool CreateNewPool(GameObject prefab)
        {
            ObjectPool pool = new ObjectPool();
            pool.PoolType = typeof(GameObject);
            pool.ObjectName = prefab.name;
            _pools.Add(pool);
            return pool;
        }

        /// <summary>
        /// Used by ObjectPools to instantiate new objects since ObjectPool does not inherit from monobehaviour and thus cannot instantiate objects self.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="gameObject"></param>
        /// <returns></returns>
        internal static T ObjectCreater<T>(MonoBehaviour gameObject) where T : MonoBehaviour
        {
            T newobject = (T)Instantiate(gameObject) as T;
            return newobject;
        }

        /// <summary>
        /// Used by ObjectPools to instantiate new objects since ObjectPool does not inherit from monobehaviour and thus cannot instantiate objects self.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="gameObject"></param>
        /// <returns></returns>
        internal static GameObject ObjectCreater(GameObject gameObject)
        {
            GameObject newobject = (GameObject)Instantiate(gameObject);
            return newobject;
        }

        /// <summary>
        /// Gets object of given type by either creating a new or enabling an existing, disabled object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="prefab"></param>
        /// <returns></returns>
        public static T Get<T>(MonoBehaviour prefab) where T : MonoBehaviour
        {
            ObjectPool objPool = GetPool(prefab.name, typeof(MonoBehaviour));

            if (objPool == null)
            {
                // create new pool
                objPool = CreateNewPool<T>(prefab);
            }

            T newObject = objPool.GetObject<T>(prefab);
            newObject.gameObject.SetActive(true);

            return newObject;
        }

        /// <summary>
        /// Generic replacement for GameObject.Instantiate
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="prefab"></param>
        /// <param name="position"></param>
        /// <param name="quaternion"></param>
        /// <returns></returns>
        public static T Get<T>(T prefab, Vector3 position, Quaternion quaternion) where T : MonoBehaviour
        {
            ObjectPool objPool = GetPool(prefab.name, typeof(T));

            if (objPool == null)
            {
                // create new pool
                objPool = CreateNewPool<T>(prefab);
            }

            T newObject = objPool.GetObject<T>(prefab);
            newObject.gameObject.SetActive(true);
            newObject.transform.position = position;
            newObject.transform.rotation = quaternion;

            return newObject;
        }

        /// <summary>
        /// Gets an instance of the prefab by either creating a new or enabling an existing, disabled object
        /// </summary>
        /// <param name="prefab"></param>
        /// <returns></returns>
        public static GameObject Get(GameObject prefab)
        {
            ObjectPool objPool = GetPool(prefab.name, typeof(GameObject));

            if (objPool == null)
            {
                // create new pool
                objPool = CreateNewPool(prefab);
            }

            GameObject newObject = objPool.GetObject(prefab);
            newObject.SetActive(true);

            return newObject;
        }

        /// <summary>
        /// Replacement for GameObject.Instantiate
        /// </summary>
        /// <param name="prefab"></param>
        /// <param name="position"></param>
        /// <param name="quaternion"></param>
        /// <returns></returns>
        public static GameObject Get(GameObject prefab, Vector3 position, Quaternion quaternion)
        {
            ObjectPool objPool = GetPool(prefab.name, typeof(GameObject));

            if (objPool == null)
            {
                // create new pool
                objPool = CreateNewPool(prefab);
            }

            GameObject newObject = objPool.GetObject(prefab);
            newObject.gameObject.SetActive(true);
            newObject.transform.position = position;
            newObject.transform.rotation = quaternion;

            return newObject;
        }
		
		public static void DisableAllInPool(GameObject prefab)
		{
			ObjectPool objPool = GetPool(prefab.name, typeof(GameObject));

            if (objPool == null)
            {
                Debug.LogError(prefab.name + " Does not exist in the current pool data");
            }
			else
			{
				for(int i = 0; i < objPool.ObjectCount ;i++)
				{
					objPool._PoolGameObjects[i].SetActive(false);
				}
			}
		}

        /// <summary>
        /// Gets the pool to get objects from
        /// </summary>
        /// <param name="name">prefab name</param>
        /// <param name="type">Specific type or standard GameObject</param>
        /// <returns>An existing object pool or, failing that, null.</returns>
        private static ObjectPool GetPool(string name, Type type)
        {
            var matchingPools = _pools.Where(x => x.ObjectName == name && x.PoolType == type);

            if (matchingPools.Any())
            {
                return matchingPools.First();
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// If option for cleaning pools is set to true, this method will clean the pools (running constantly).
        /// Upon cleaning a stale pool all deactivated objects are destroyed.
        /// Deletes the pool if there are no more objects left in it
        /// </summary>
        /// <returns></returns>
        private IEnumerator CleanPools()
        {
            while (true)
            {
                List<ObjectPool> poolsToDelete = new List<ObjectPool>();

                foreach (ObjectPool pool in _pools)
                {
                    if (pool.lastUsed + SecondsStaleBeforeCleanup < Time.timeSinceLevelLoad)
                    { 
                        // the pool has gone stale
                        pool.RemoveDisabledObjects();

                        if (pool.ObjectCount < 1)
                        {
                            poolsToDelete.Add(pool);
                        }
                    }
                }

                poolsToDelete.ForEach(x => _pools.Remove(x));

                yield return new WaitForSeconds(_secondsPoolRefreshRate);
            }
        }
    }
}
