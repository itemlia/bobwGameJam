using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerAsteroidController : MonoBehaviour
{
    [Header("components")] 
    [SerializeField] private PlayerControls controls;

    private Vector3 mousePos;

    private void Awake()
    {
        controls =  new PlayerControls();
    }

    private void OnEnable()
    {
        controls.Enable();

        controls.asteroidInput.leftClick.performed += Handle_Spawn;
        controls.asteroidInput.leftClick.canceled += Handle_SpawnCancelled;
    }
    
    private void OnDisable()
    {
        controls.Disable();

        controls.asteroidInput.leftClick.performed -= Handle_Spawn;
        controls.asteroidInput.leftClick.canceled -= Handle_SpawnCancelled;
    } 
    
    
    private void Handle_Spawn(InputAction.CallbackContext obj)
     {
         mousePos = obj.ReadValue<Vector2>();
     }
    
    private void Handle_SpawnCancelled(InputAction.CallbackContext obj)
    {
        mousePos = Vector2.zero;
    }
}
