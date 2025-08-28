using System;
using UnityEngine;

public class PooledObjects : MonoBehaviour
{
    [HideInInspector] public int poolIndex { get; private set; }
    public bool m_active;
    designPatternsObjectPooler poolerRef;

    //init chain to initialize the object
    public void init(int poolIndex, designPatternsObjectPooler poolRef)
    {
        this.poolIndex = poolIndex;
        poolerRef = poolRef;
    }

    // recycle function -> call to put this object back in the right pool
    public void recycleSelf()
    {
        poolerRef.RecycleObject(this);
    }

    // clean up for object
    private void OnDestroy()
    {
       poolerRef.OnPoolCleanup -= this.recycleSelf;
    }
}
