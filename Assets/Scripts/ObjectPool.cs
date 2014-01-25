using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Pool
{
    /// <summary>
    /// Object pool of a specific object type or prefab.
    /// Object name and pool type are the unique identifiers of a pool.
    /// </summary>
    internal class ObjectPool
    {
        public Type PoolType;
        public int ObjectCount { get { return _poolObjects.Count + _poolGameObjects.Count; } }
        public string ObjectName = "";
        internal float lastUsed = 0;
        //private static Vector3 _hidingSpot = new Vector3(0, 0, 0);
        private List<MonoBehaviour> _poolObjects = new List<MonoBehaviour>();
        private List<GameObject> _poolGameObjects = new List<GameObject>();

        /// <summary>
        /// Gets a deactivated of type object or creates a new one
        /// </summary>
		
		private T CreateNewObject<T>(MonoBehaviour objectPrefab) where T : MonoBehaviour
        {
            var returnedObject = PoolManager.ObjectCreater<T>(objectPrefab);
            _poolObjects.Add(returnedObject);
            return returnedObject;
        }
		
        public T GetObject<T>(MonoBehaviour objectPrefab) where T : MonoBehaviour
        {
            T retrievedObject = null;

            var disabledObjects = _poolObjects.Where(x => x.gameObject.activeSelf == false);

            if (disabledObjects.Any())
            {
                retrievedObject = (T)disabledObjects.First();
            }
            else
            {
                retrievedObject = CreateNewObject<T>(objectPrefab);
            }

            retrievedObject.gameObject.SetActive(true);
            lastUsed = Time.timeSinceLevelLoad;
            return retrievedObject;
        }


        /// <summary>
        /// Gets a deactivated of type object or creates a new one
        /// </summary>
        public GameObject GetObject(GameObject prefab)
        {
            GameObject retrievedObject = null;

            var disabledObjects = _poolGameObjects.Where(x => x.activeSelf == false);

            if (disabledObjects.Any())
            {
                retrievedObject = disabledObjects.First();
            }
            else
            {
                retrievedObject = CreateNewObject(prefab);
            }

            retrievedObject.gameObject.SetActive(true);
            lastUsed = Time.timeSinceLevelLoad;
            return retrievedObject;
        }

        private GameObject CreateNewObject(GameObject objectPrefab)
        {
            var returnedObject = PoolManager.ObjectCreater(objectPrefab);
            _poolGameObjects.Add(returnedObject);
            return returnedObject;
        }


        /// <summary>
        /// Removes disabled objects as part of the cleanup process. Removed objects are destroyed by GameObject.Destroy()
        /// </summary>
        internal void RemoveDisabledObjects()
        {
            var disabledPoolobjectsByT = _poolObjects.Where(poolObject => poolObject.gameObject.activeSelf == false).ToList();
            _poolObjects = _poolObjects.Where(poolObject => poolObject.gameObject.activeSelf == true).ToList();

            if (disabledPoolobjectsByT != null)
            {
                foreach (MonoBehaviour poolobject in disabledPoolobjectsByT)
                {
                    GameObject.Destroy(poolobject);
                }
            }

            var disabledPoolobjects = _poolGameObjects.Where(poolObject => poolObject.gameObject.activeSelf == false).ToList();
            _poolGameObjects = _poolGameObjects.Where(poolObject => poolObject.gameObject.activeSelf == true).ToList();

            if (disabledPoolobjects != null)
            {
                foreach (GameObject poolobject in disabledPoolobjects)
                {
                    GameObject.Destroy(poolobject);
                }
            }

            lastUsed = Time.timeSinceLevelLoad;
        }
		
		public List<GameObject> _PoolGameObjects
		{
			get{ return _poolGameObjects; }	
		}
			
    }
}
