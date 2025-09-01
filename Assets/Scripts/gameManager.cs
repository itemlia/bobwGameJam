using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour, IPoints
{
    public event Action<float> onPointsGained;

    [SerializeField] private float pointValue;
    [SerializeField] public List<string> scenes;
    
    private void Awake()
    {
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
