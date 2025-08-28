using System;
using UnityEngine;

public class PooledObjects : MonoBehaviour
{
    [HideInInspector] public int m_poolIndex { get; private set; }
    public bool m_active;
    designPatternsObjectPooler m_poolerRef;

    //init chain to initialize the object
    public void init(int poolIndex, designPatternsObjectPooler poolRef)
    {
        m_poolIndex = poolIndex;
        m_poolerRef = poolRef;
        //m_active = false;
    }

    // our recycle function which we want to call to put this object back in the right pool
    public void recycleSelf()
    {
        m_poolerRef.RecycleObject(this);
    }

    // clean up for our object
    private void OnDestroy()
    {
       m_poolerRef.OnPoolCleanup -= this.recycleSelf;
    }
}
