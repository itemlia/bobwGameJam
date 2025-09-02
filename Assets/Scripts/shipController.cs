using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = System.Random;

public class shipController : MonoBehaviour
{
   [Header("components")]
   [SerializeField] private Rigidbody2D rb;
   [SerializeField] private gameManager gameManager;
   [SerializeField] private healthComponent healthComp;
   [SerializeField] private AudioSource audioAsteroid;
   [SerializeField] private AudioSource audioShip;
   [SerializeField] private GameObject particles;
   
   [Header("movement")]
   [SerializeField] private GameObject[] movePoints;
   [SerializeField] private float speed = 10.0f;


   private int index;


   private void OnEnable()
   {
      healthComp.onDamaged += Handle_HealthDamaged;
      healthComp.onDead += Handle_OnDead;
   }

   private void OnDisable()
   {
      healthComp.onDamaged -= Handle_HealthDamaged;
      healthComp.onDead -= Handle_OnDead;
   }

   private void Awake()
   {
      randomPoint();
   }

   private void Start()
   {
      rb =  GetComponent<Rigidbody2D>();
      healthComp = GetComponent<healthComponent>();
      gameManager = GameObject.FindWithTag("gm").GetComponent<gameManager>();
   }

   private void Update()
   {
      moveTo();
   }

   private void randomPoint()
   {
      Random rand =  new Random();
      index = rand.Next(0, movePoints.Length);
      
      //generates a point to move to from array of available points
   }

   //made function recursive so calls itself once ship meets MoveTowards point
   private void moveTo()
   {
      while (true)
      {
         if (gameObject.transform.position != movePoints[index].transform.position) //if ship isnt at generated point -> move to point
         {
            Vector3 moveTo = movePoints[index].transform.position;

            float step = speed * Time.deltaTime;

            // move ship towards the target location
            transform.position = Vector2.MoveTowards(transform.position, moveTo, step);
         }
         else
         {
            randomPoint(); //generate new point to move to
            continue;
         }

         break;
      }
   }

   private void Handle_HealthDamaged(float currentHealth, float maxHealth, float changedHeath)
   {
      float healthVal = ((currentHealth / maxHealth) * 100);
      uiCon.changeBarVal(healthVal);
      gameManager.applyPoints(10);
   }
   
   private void Handle_OnDead(MonoBehaviour causer)
   {
      StartCoroutine(playClip());
   }
   
   private IEnumerator playClip()
   {
      AudioSource.PlayClipAtPoint(audioShip.clip, transform.position, 0.8f);

      gameObject.GetComponent<SpriteRenderer>().enabled = false;
      
      Instantiate(particles, transform.position, Quaternion.identity);
        
      yield return new WaitForSeconds(5f);

      SceneManager.LoadScene("Scenes/winScreen");
   }
   
   private void OnTriggerEnter2D(Collider2D other)
   {
      Debug.Log("hit");
      
      if (other.gameObject.CompareTag("Asteroid"))
      {
         AudioSource.PlayClipAtPoint(audioAsteroid.clip, transform.position, 0.8f);
         
         healthComp.applyDamage(10, other.gameObject.GetComponent<MonoBehaviour>());
         
         Destroy(other.gameObject);
      }
      else if (other.gameObject.CompareTag("End"))
      {
         SceneManager.LoadScene("Scenes/loseScreen");
      }
   }
}
