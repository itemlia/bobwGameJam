using System;
using System.Collections.Generic;
using UnityEngine;
 
 
public class designPatternsObjectPooler : MonoBehaviour
{
    [Serializable]
    private struct PooledObjectData
    {
        public GameObject Prefab;
        public string Name;
        public int PoolSize;
        public bool CanGrow;
    }
 
    public event Action OnPoolCleanup;
 
 
    [SerializeField] private PooledObjectData[] m_ObjectData;
    private List<PooledObjects>[] m_PooledObjects;
    private GameObject[] m_Pools;
 
 //setup  "pools" using empty GameObjects in hierarchy
    private void Awake()
    {
        int poolNum = m_ObjectData.Length;
        m_Pools = new GameObject[poolNum];
        m_PooledObjects = new List<PooledObjects>[poolNum];
 
 
        for (int poolIndex = 0; poolIndex < poolNum; poolIndex++)
        {
            //Create the pool parents for Hierarchy organisation
            GameObject Pool = new GameObject($"Pool: {m_ObjectData[poolIndex].Name}");
            Pool.transform.parent = transform;
            m_Pools[poolIndex] = Pool;
            m_PooledObjects[poolIndex] = new List<PooledObjects>();
            for (int objectIndex = 0; objectIndex < m_ObjectData[poolIndex].PoolSize; objectIndex++)
            {
                SpawnObject(poolIndex, objectIndex);
            }
        }
    }
 
 //our public function to get objects during runtime
    public GameObject GetPooledObject(string name)
    {
        int poolCount = m_Pools.Length;
        int targetPool = -1;
        for (int poolIndex = 0; poolIndex < poolCount; poolIndex++)
        {
            if (m_Pools[poolIndex].name == $"Pool: {name}")
            {
                targetPool = poolIndex;
                break;
            }
        }
 
 
        Debug.Assert(targetPool >= 0, $"No Pool for objects by the name of {name}");
 
 
        int objectCount = m_PooledObjects[targetPool].Count;
        int targetObject = -1;
 
 
        for (int objectIndex = 0; objectIndex < objectCount; objectIndex++)
        {
            if (m_PooledObjects[targetPool][objectIndex] != null)
            {
                if (!m_PooledObjects[targetPool][objectIndex].m_active)
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
            if (m_ObjectData[targetPool].CanGrow)
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
        PooledObjects toReturn = m_PooledObjects[targetPool][targetObject];
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
        poolRef.transform.SetParent(m_Pools[poolRef.m_poolIndex].transform);
        poolRef.gameObject.SetActive(false);
        poolRef.m_active = false;
        OnPoolCleanup -= poolRef.recycleSelf;
    }
 
 // spawn all objects in to pools and if its a new object that was added at runtime because ran out of objects and CanGrow was enabled, can insert them to the right list
    private PooledObjects SpawnObject(int poolIndex, int objectIndex)
    {
        GameObject tempGO = Instantiate(m_ObjectData[poolIndex].Prefab, m_Pools[poolIndex].transform);
        PooledObjects pooledRef = tempGO.AddComponent<PooledObjects>();
        tempGO.name = m_ObjectData[poolIndex].Name;
        
        tempGO.SetActive(false);
        if (objectIndex >= m_PooledObjects[poolIndex].Count)
        {
            m_PooledObjects[poolIndex].Add(pooledRef);
        }
        else
        {
            m_PooledObjects[poolIndex].Insert(objectIndex, pooledRef);
        }
        pooledRef.init(poolIndex, this);
        return pooledRef;
    }
}