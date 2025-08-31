using System;
using UnityEngine;

public class cameraController : MonoBehaviour
{
    [Header("components")]
    [SerializeField] private Camera camera;
    [SerializeField] private EdgeCollider2D edgeColl;
    private void Awake()
    {
        camera = GetComponent<Camera>();

        camera.rect = new Rect(0, 0, 1, 1);

        edgeColl = GetComponent<EdgeCollider2D>();

        camEdgeCollider();
    }

    private void camEdgeCollider() 
    {
        camera = Camera.main;
    
        if (!camera.orthographic)
        {
            Debug.LogError("main camera is not orthographic, failed to create edge colliders"); 
            return;
        }
        
        Vector2 leftBottom = camera.ScreenToWorldPoint(new Vector3(0, 0, camera.nearClipPlane));
        Vector2 leftTop = camera.ScreenToWorldPoint(new Vector3(0, Screen.height, camera.nearClipPlane));
        Vector2 rightTop = camera.ScreenToWorldPoint(new Vector3(1, 1, camera.nearClipPlane));
        Vector2 rightBottom = camera.ScreenToWorldPoint(new Vector3(Screen.width, 0, camera.nearClipPlane));

        
        Vector2[] edgePoints =
        {
            leftBottom,
            leftTop,
            rightTop,
            rightBottom,
            leftBottom,
        };
        
        edgeColl.points = edgePoints;
    }
}

