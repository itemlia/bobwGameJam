using System;
using System.Collections;
using UnityEngine;

public class asteroid : MonoBehaviour
{
    [SerializeField] gameManager gameManager;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private GameObject particles;

    private void Start()
    {
        gameManager = GameObject.Find("gameManager").GetComponent<gameManager>();
        audioSource = GetComponent<AudioSource>();
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            StartCoroutine(play());
            
            gameManager.applyPoints(10);
               
            Destroy(other.gameObject);
            Destroy(gameObject);
            
            Instantiate(particles, transform.position, Quaternion.identity);
        }
        
        Destroy(gameObject);
    }

    private IEnumerator play()
    {    
        AudioSource.PlayClipAtPoint(audioSource.clip, transform.position, 1);   
        
        yield return new WaitForSeconds(5f);
    }
}
