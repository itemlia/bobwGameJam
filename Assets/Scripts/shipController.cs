using System;
using UnityEngine;
using Random = System.Random;

public class shipController : MonoBehaviour
{
   [SerializeField] private GameObject[] movePoints;
   [SerializeField] private float speed = 10.0f;
   [SerializeField] private Vector2 position;

   [SerializeField] private Rigidbody2D rb;

   private void Start()
   {
      position = gameObject.transform.position;
      rb =  GetComponent<Rigidbody2D>();
   }

   private void Update()
   {
      rb.linearVelocity = toMoveTo() * speed;
   }

   private Vector3 toMoveTo()
   {
      Random rand =  new Random();
      int index = rand.Next(0, movePoints.Length);
      
      Vector3 moveTo = Vector3.MoveTowards(position, movePoints[index].transform.position, Time.deltaTime * speed);
      
      return moveTo;
   }
}
