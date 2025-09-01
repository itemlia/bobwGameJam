using System;
using UnityEngine;

public class asteroid : MonoBehaviour
{
    [SerializeField] gameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.Find("gameManager").GetComponent<gameManager>();
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            gameManager.applyPoints(10);
            Destroy(other.gameObject);
            Destroy(gameObject);
        } 
        Destroy(gameObject);
    }
}
