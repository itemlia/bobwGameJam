using System;
using System.Collections.Generic;
using UnityEngine;
 
 
public class designPatternsObjectPooler : MonoBehaviour
{
    [Serializable]
    private struct PooledObjectData
    {
        public GameObject prefab;
        public string name;
        public int poolSize;
        public bool canGrow;
    }
 
    public event Action OnPoolCleanup;
 
 
    [SerializeField] private PooledObjectData[] objectData;
    private List<PooledObjects>[] pooledObjects;
    private GameObject[] Pools;
 
 //setup "pools" using empty GameObjects in hierarchy
    private void Awake()
    {
        int poolNum = objectData.Length;
        Pools = new GameObject[poolNum];
        pooledObjects = new List<PooledObjects>[poolNum];
 
 
        for (int poolIndex = 0; poolIndex < poolNum; poolIndex++)
        {
            //Create the pool parents for Hierarchy organisation
            GameObject Pool = new GameObject($"Pool: {objectData[poolIndex].name}");
            Pool.transform.parent = transform;
            Pools[poolIndex] = Pool;
            pooledObjects[poolIndex] = new List<PooledObjects>();
            for (int objectIndex = 0; objectIndex < objectData[poolIndex].poolSize; objectIndex++)
            {
                SpawnObject(poolIndex, objectIndex);
            }
        }
    }
 
 //our public function to get objects during runtime
    public GameObject GetPooledObject(string name)
    {
        int poolCount = Pools.Length;
        int targetPool = -1;
        for (int poolIndex = 0; poolIndex < poolCount; poolIndex++)
        {
            if (Pools[poolIndex].name == $"Pool: {name}")
            {
                targetPool = poolIndex;
                break;
            }
        }
 
 
        Debug.Assert(targetPool >= 0, $"No Pool for objects by the name of {name}");
 
 
        int objectCount = pooledObjects[targetPool].Count;
        int targetObject = -1;
 
 
        for (int objectIndex = 0; objectIndex < objectCount; objectIndex++)
        {
            if (pooledObjects[targetPool][objectIndex] != null)
            {
                if (!pooledObjects[targetPool][objectIndex].m_active)
                {
                    targetObject = objectIndex;
                    break;
                }
            }
            else
            {
                SpawnObject(targetPool, objectIndex);
                targetObject = objectIndex;
                break;
            }
        }
 
        //catch if we run out of Objects in our "pool",
        //if run out and CanGrow is set to true -> can spawn a new object and give it an ID -> If its set to false then just return a warning message
        if (targetObject == -1)
        {
            if (objectData[targetPool].canGrow)
            {
                SpawnObject(targetPool, objectCount);
                targetObject = objectCount;
            }
            else
            {
                Debug.LogWarning($"No {name} objects left in pool and no option for pool to grow. Returning NULL.");
                return null;
            }
        }
 
        //return the object from the right pool, and bind it to the clean up event
        PooledObjects toReturn = pooledObjects[targetPool][targetObject];
        toReturn.m_active = true;
        OnPoolCleanup += toReturn.recycleSelf;
        return toReturn.gameObject;
    }
 
    //catch any objects that are tried to be sent back to the pool incorrectly,  can recylce it if it has a PooledObject component or throw out an error if it doesn't 
    public void RecycleObject(GameObject obj)
    {
        PooledObjects poolRef = obj.GetComponent<PooledObjects>();
        Debug.Assert(poolRef != null, $"Trying to recycle an object called {obj.name} that didnt come from the Object Pooler");
        RecycleObject(poolRef);
    }
 
    //recycle pooled objects
    public void RecycleObject(PooledObjects poolRef)
    {
        poolRef.transform.SetParent(Pools[poolRef.poolIndex].transform);
        poolRef.gameObject.SetActive(false);
        poolRef.m_active = false;
        OnPoolCleanup -= poolRef.recycleSelf;
    }
 
 // spawn all objects in to pools and if its a new object that was added at runtime because ran out of objects and CanGrow was enabled, can insert them to the right list
    private PooledObjects SpawnObject(int poolIndex, int objectIndex)
    {
        GameObject tempGO = Instantiate(objectData[poolIndex].prefab, Pools[poolIndex].transform);
        PooledObjects pooledRef = tempGO.AddComponent<PooledObjects>();
        tempGO.name = objectData[poolIndex].name;
        
        tempGO.SetActive(false);
        if (objectIndex >= pooledObjects[poolIndex].Count)
        {
            pooledObjects[poolIndex].Add(pooledRef);
        }
        else
        {
            pooledObjects[poolIndex].Insert(objectIndex, pooledRef);
        }
        pooledRef.init(poolIndex, this);
        return pooledRef;
    }
}