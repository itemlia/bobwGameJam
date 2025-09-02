using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class playerShipController : MonoBehaviour
{
    [Header("components")]
    [SerializeField] private PlayerControls controls;
    [SerializeField] private designPatternsObjectPooler objectPooler;
    [SerializeField] private healthComponent healthComp;
    [SerializeField] private gameManager gameManager;
    [SerializeField] private uiCon uiController;
    [SerializeField] private AudioSource audioSource;

    private Vector3 inputMove;
    private Rigidbody2D rb;
    
    [Header("movement variables")]
    [SerializeField] private float speed;
    

    private void Awake()
    {
        controls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
        objectPooler = GameObject.Find("objectPooler").GetComponent<designPatternsObjectPooler>();
        healthComp = GetComponent<healthComponent>();
        gameManager = GameObject.Find("gameManager").GetComponent<gameManager>();
        uiController = GameObject.Find("ui").GetComponent<uiCon>();
        audioSource = GetComponent<AudioSource>();
        
    }

    private void OnEnable()
    {
        controls.Enable();

        controls.shipInput.Move.performed += Handle_MovePerformed;
        controls.shipInput.Move.canceled += Handle_MoveCancelled;
        controls.shipInput.Shoot.performed += Handle_ShootPerformed;
        controls.shipInput.Shoot.canceled += Handle_ShootCancelled;

        healthComp.onDamaged += Handle_HealthDamaged;
        healthComp.onDead += Handle_OnDead;

        gameManager.onPointsGained += Handle_PointsGained;
    }



    private void OnDisable()
    {
        controls.Disable();

        controls.shipInput.Move.performed -= Handle_MovePerformed;
        controls.shipInput.Move.canceled -= Handle_MoveCancelled;
        controls.shipInput.Shoot.performed -= Handle_ShootPerformed;
        controls.shipInput.Shoot.canceled -= Handle_ShootCancelled;
        
        healthComp.onDamaged -= Handle_HealthDamaged;
        healthComp.onDead -= Handle_OnDead;
        
        gameManager.onPointsGained -= Handle_PointsGained;
    }


    private void Handle_MovePerformed(InputAction.CallbackContext context)
    {
        inputMove = context.ReadValue<Vector2>();    
    }
    private void Handle_MoveCancelled(InputAction.CallbackContext obj)
    {
        inputMove = Vector2.zero;
    }
    
    private void Handle_ShootPerformed(InputAction.CallbackContext obj)
    {
        GameObject bulletToSpawn = objectPooler.GetPooledObject("bullet");

        if (bulletToSpawn == null) { return; }
		
        bulletToSpawn.SetActive(true);
        bulletToSpawn.transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, 0);
        bulletToSpawn.GetComponent<bullet>().init();
        
        AudioSource.PlayClipAtPoint(audioSource.clip, transform.position, 0.3f);
    }
    private void Handle_ShootCancelled(InputAction.CallbackContext obj)
    {
    }

    private void Handle_HealthDamaged(float currentHealth, float maxHealth, float changedHeath)
    {
        float healthVal = ((currentHealth / maxHealth) * 100);
        uiCon.changeBarVal(healthVal);
    }
    
    
    private void Handle_OnDead(MonoBehaviour causer)
    {
        Destroy(gameObject);
        SceneManager.LoadScene("Scenes/loseScreen");
    }

    private void Handle_PointsGained(float pointsGained)
    {
        uiController.setPoints(pointsGained);
    }
    private void Update()
    {
        rb.linearVelocity = inputMove * speed;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Asteroid"))
        {
            healthComp.applyDamage(10, other.gameObject.GetComponent<MonoBehaviour>());
            gameManager.applyPoints(10);
            Destroy(other.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("End"))
        {
            SceneManager.LoadScene("Scenes/winScreen");
        }
    }
}
