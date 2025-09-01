using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;


public class uiConPause : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    private PlayerControls controls;

    private Button bHome;
    private Button bQuit;
    private Button bResume;
    


    private void Awake()
    {
        controls = new PlayerControls();
        
        pauseMenu.SetActive(false);
        
    }

    private void OnEnable()
    {
        controls.Enable();

        controls.pause.pause.performed += Handle_Pause;
        controls.pause.pause.canceled += Handle_UnPause;
    }


    private void OnDisable()
    {
        controls.Disable();

        controls.pause.pause.performed -= Handle_Pause;
        controls.pause.pause.canceled -= Handle_UnPause;
    }
   
    
    private void Handle_Pause(InputAction.CallbackContext obj)
    {
        if (SceneManager.GetActiveScene().name == "asteroidLevelOne" || SceneManager.GetActiveScene().name == "levelOne")
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
        }
        
    }
    
    private void Handle_UnPause(InputAction.CallbackContext obj)
    {
        
    }


}
