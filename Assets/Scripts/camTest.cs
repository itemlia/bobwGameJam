using System;
using UnityEngine;

public class camTest : MonoBehaviour
{
    private Camera camera;
    
    [SerializeField] private BoxCollider2D leftCollider;
    [SerializeField] private BoxCollider2D topCollider;
    [SerializeField] private BoxCollider2D rightCollider;
    [SerializeField] private BoxCollider2D bottomCollider;

    private void Awake()
    {
      
    }

    private void Start()
    {
        camera = GetComponent<Camera>();

        if (!camera.orthographic)
        {
            Debug.LogError("camera not orthographic");
        }
    }

    private void Update()
    {
        SetCameraEdges();
    }

    private void SetCameraEdges()
    {
        float halfHeight = camera.orthographicSize;
        float halfWidth = camera.orthographicSize * camera.aspect;
        
        leftCollider.offset = new Vector3(-halfWidth - leftCollider.size.x * 0.5f, 0f, 10f);
        leftCollider.size = new Vector3(leftCollider.size.x, halfHeight * 2f, 20f);

        topCollider.offset = new Vector3(0f, halfHeight + topCollider.size.y * 0.5f, 10f);
        topCollider.size = new Vector3(halfWidth * 2f + topCollider.size.y * 2f, topCollider.size.y, 20f);

        rightCollider.offset = new Vector3(leftCollider.offset.x * -1f, leftCollider.offset.y, 0);
        rightCollider.size = leftCollider.size;

        bottomCollider.offset = new Vector3(topCollider.offset.x, topCollider.offset.y * -1f, 0);
        bottomCollider.size = topCollider.size;
    }
    
    
}
