using System;
using Unity.VisualScripting;
using UnityEngine;

public class gameManager : MonoBehaviour, IPoints
{
    public event Action<float> onPointsGained; 

    public void applyPoints(float pointNumber)
    {
        
    }
}
