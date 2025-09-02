using System;
using UnityEngine;

public class asteroid : MonoBehaviour
{
    [SerializeField] gameManager gameManager;
    [SerializeField] private AudioSource audioSource;

    private void Start()
    {
        gameManager = GameObject.Find("gameManager").GetComponent<gameManager>();
        audioSource = GetComponent<AudioSource>();
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        
        
        if (other.gameObject.CompareTag("Bullet"))
        {
            gameManager.applyPoints(10);
            AudioSource.PlayClipAtPoint(audioSource.clip, transform.position, 1);            
            Destroy(other.gameObject);
            Destroy(gameObject);
        } 
        Destroy(gameObject);
    }
}
