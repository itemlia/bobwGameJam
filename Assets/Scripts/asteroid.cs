using System;
using UnityEngine;

public class asteroid : MonoBehaviour
{
    [SerializeField] gameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.Find("gameManager").GetComponent<gameManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
            gameManager.applyPoints(10);
        }    
    }
    
}
