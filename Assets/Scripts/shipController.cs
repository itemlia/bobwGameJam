using System;
using UnityEngine;
using Random = System.Random;

public class shipController : MonoBehaviour
{
   [SerializeField] private GameObject[] movePoints;
   [SerializeField] private float speed = 10.0f;

   [SerializeField] private Rigidbody2D rb;

   private int index;


   private void Awake()
   {
      randomPoint();
   }

   private void Start()
   {
      rb =  GetComponent<Rigidbody2D>();
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
}
