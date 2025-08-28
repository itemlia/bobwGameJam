using System;
using Unity.VisualScripting;
using UnityEngine;

public class gameManager : MonoBehaviour, IPoints
{
    public event Action<float> onPointsGained;

    [SerializeField] private float pointValue;

    public void applyPoints(float pointsGained)
    {
        float newPointValue = pointValue + pointsGained;
        pointValue = newPointValue;
        onPointsGained?.Invoke(pointValue);
    }
}
