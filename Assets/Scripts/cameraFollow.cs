using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Vector3 cameraOffset;

    private void Start()
    {
        player = GameObject.Find("player").GetComponent<Transform>();
    }

    private void Update() //in update to stop camera moving before playerTrans does
    {
        transform.position = player.position + cameraOffset; //camera follows player around the scene
    }
}
