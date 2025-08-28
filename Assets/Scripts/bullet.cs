using System;
using UnityEngine;
using Vector2 = System.Numerics.Vector2;

public class bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float force;

    private PooledObjects pooledObjects;

    public void Init(float orientation)
    {
        if (orientation.Equals(1))
        {
          rb.AddForce(UnityEngine.Vector2.right * force, ForceMode2D.Impulse);
          
        }
        else 
        {
            rb.AddForce(UnityEngine.Vector2.left * force, ForceMode2D.Impulse);
        }
    }

    private void Start()
    {
        pooledObjects = GetComponent<PooledObjects>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.transform.CompareTag("Player"))
        {
            pooledObjects.recycleSelf();
        }
    }
}
