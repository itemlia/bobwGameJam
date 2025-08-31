using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class objectController : MonoBehaviour
{
    [SerializeField] private Transform tTopWall;
    [SerializeField] private Transform tPlayer;

    [SerializeField] private float timer;
    [SerializeField] private float offset;

    [SerializeField] private GameObject debris;



    private IEnumerator dropTimer()
    {
        while (tPlayer.position.y <= tTopWall.position.y)
        {
            yield return new WaitForSeconds(timer);
            Instantiate(debris, new Vector3(tTopWall.position.x + offset, tTopWall.position.y, 0f), Quaternion.identity);
        }
    }

    private void Start()
    {
        tTopWall = GameObject.Find("rock").GetComponent<Transform>();
        tPlayer = GameObject.Find("player").GetComponent<Transform>();

        StartCoroutine(dropTimer());
    }

    
}
