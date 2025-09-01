using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour, IPoints
{
    public event Action<float> onPointsGained;
    
    private PlayerControls controls;


    [SerializeField] private float pointValue;
    [SerializeField] public List<string> scenes;
    
    private void Awake()
    {
        controls = new PlayerControls();

        
        GameObject[] managers = GameObject.FindGameObjectsWithTag("gm");
                  
          if (managers.Length > 1)
          {
              Destroy(managers[1]);
          }
          else
          {
              DontDestroyOnLoad(gameObject);
          }
          
        SceneManager.sceneLoaded += onSceneLoaded; 
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
        SceneManager.LoadScene("Scenes/titleScreen");
        
    }
    private void Handle_UnPause(InputAction.CallbackContext obj)
    {
        
    }
    public void applyPoints(float pointsGained)
    {
        float newPointValue = pointValue + pointsGained;
        pointValue = newPointValue;
        onPointsGained?.Invoke(pointValue);
    }

    private void onSceneLoaded(Scene scene, LoadSceneMode mode)
    {  
        var activeScene = SceneManager.GetActiveScene();
        scenes.Add(activeScene.name);
    }
}
