using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public enum PoolableObjects
{
    Ball
}

public class ObjectPoolManager : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public PoolableObjects poolableObject;
        public GameObject prefab;
        public int size;
        public bool shouldExpand;
    }

    #region Vars

    public List<Pool> pools = new List<Pool>();

    public Dictionary<PoolableObjects, Queue<GameObject>> poolDictionary = new Dictionary<PoolableObjects, Queue<GameObject>>();

    public static ObjectPoolManager instance;

    #endregion
    

    #region Awake

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        
        CreatePools();
    }

    #endregion


    #region Pooling

    public GameObject SpawnFromPool(PoolableObjects poolableObject, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(poolableObject))
        {
            Debug.LogWarning("Tag does not exist in pool");
            return null;
        }

         GameObject objectToSpawn = poolDictionary[poolableObject].Dequeue();

        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        IPooledObject pooledObject = objectToSpawn.GetComponent<IPooledObject>();
        pooledObject.OnObjectSpawned();
        
        poolDictionary[poolableObject].Enqueue(objectToSpawn);

        return objectToSpawn;
    }

    public void ReturnToPool(GameObject pooledObj)
    {
        pooledObj.SetActive(false);
    }

    void CreatePools()
    {
        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            
            GameObject parent = new GameObject(pool.poolableObject.ToString());
            parent.transform.SetParent(transform);

            for (int i = 0; i < pool.size; i++)
            {
                var obj = CreatePooledObject(pool, parent.transform, i);
                objectPool.Enqueue(obj);
            }
            
            poolDictionary.Add(pool.poolableObject, objectPool);
        }
    }

    private GameObject CreatePooledObject(Pool pool, Transform parent, int idx)
    {
        GameObject obj = Instantiate(pool.prefab, parent, false);
        obj.name = pool.poolableObject + " ID " + idx;
        obj.SetActive(false);

        return obj;
    }
    
    #endregion
}
