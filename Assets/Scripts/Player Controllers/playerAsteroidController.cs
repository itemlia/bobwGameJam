using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerAsteroidController : MonoBehaviour
{
    [Header("components")] 
    [SerializeField] private PlayerControls controls;
    [SerializeField] private Camera mainCamera;

    private Vector3 mousePos;
    
    [Header("objects")]
    [SerializeField] private GameObject asteroidPrefab;
    [SerializeField] private GameObject tTopWall;

    [Header("variables")]
    [SerializeField] private float countdownTimer;
    [SerializeField] private float cooldownTime;

    private void Awake()
    {
        controls =  new PlayerControls();
    }

    private void OnEnable()
    {
        controls.Enable();

        controls.asteroidInput.spawn.performed += Handle_Spawn;
        controls.asteroidInput.spawn.canceled += Handle_SpawnCancelled;
    }


    private void OnDisable()
    {
        controls.Disable();

        controls.asteroidInput.spawn.performed -= Handle_Spawn;
        controls.asteroidInput.spawn.canceled -= Handle_SpawnCancelled;
    } 
    
    private void Handle_Spawn(InputAction.CallbackContext obj)
    {
        mousePos = obj.ReadValue<Vector2>();

        if (countdownTimer == 0)
        {
            spawnAsteroid(mousePos);
            StartCoroutine(cooldown());
        }
        else if(countdownTimer > 0)
        {
            Debug.Log("cooldown");
        }
    }

    private void Handle_SpawnCancelled(InputAction.CallbackContext obj)
    {
        mousePos = Vector3.zero;
    }
    

    private void spawnAsteroid(Vector2 mousePos)
    {
        Vector2 camPos = mainCamera.ScreenToWorldPoint(mousePos);
        
        Vector2 spawnPos = new Vector2(camPos.x, tTopWall.transform.position.y);
        
        Instantiate(asteroidPrefab, spawnPos, Quaternion.identity);
    }

    private IEnumerator cooldown()
    {
        countdownTimer = cooldownTime;
        
        while (countdownTimer > 0 && countdownTimer != 0)
        {
            yield return new WaitForSeconds(1);
                   
            countdownTimer -= 1;
        }
    }
} 
