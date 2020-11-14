using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        public PoolableObjects poolableObjectType;
        public GameObject prefab;
        public int size;
        public bool shouldExpand;
    }

    #region Vars

    [SerializeField] private Transform objectPoolContainer;

    public List<Pool> pools = new List<Pool>();

    public Dictionary<PoolableObjects, List<GameObject>> poolDictionary =
        new Dictionary<PoolableObjects, List<GameObject>>();

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

    void CreatePools()
    {
        foreach (Pool item in pools)
        {
            List<GameObject> pooledGameObjects = new List<GameObject>();
            for (int i = 0; i < item.size; i++)
            {
                GameObject obj = CreateItemForObjectPool(item.prefab);
                pooledGameObjects.Add(obj);
            }

            poolDictionary[item.poolableObjectType] = pooledGameObjects;
        }
    }

    public GameObject GetPooledObject(PoolableObjects poolableObjectType)
    {
        if (!poolDictionary.TryGetValue(poolableObjectType, out var pooledObjects))
        {
            Debug.LogError("Trying to get a pooled object from a key which has no list in the pooled dictionary");
            return null;
        }
        
        foreach (var obj in pooledObjects)
        {
            if (!obj.activeInHierarchy)
            {
                return obj;
            }
        }

        Pool itemPool = pools.FirstOrDefault(x => x.poolableObjectType == poolableObjectType);
        if (itemPool == null || !itemPool.shouldExpand)
            return null;

        var item = CreateItemForObjectPool(itemPool.prefab);
        pooledObjects.Add(item);
        poolDictionary[itemPool.poolableObjectType] = pooledObjects;
        return item;
    }

    public void ReturnToPool(GameObject itemToBeReturned)
    {
        itemToBeReturned.SetActive(false);
    }

    private GameObject CreateItemForObjectPool(GameObject prefab)
    {
        GameObject newPoolItem = Instantiate(prefab, objectPoolContainer, false);
        newPoolItem.SetActive(false);
        return newPoolItem;
    }

    #endregion
}