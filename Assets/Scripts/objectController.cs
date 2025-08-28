using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class objectController : MonoBehaviour
{
    public Transform tRock;
    public Transform tPlayer;

    public float timer;
    public float offset;

    public GameObject debris;

    public bool positionReached;


    public IEnumerator dropTimer()
    {
        while (tPlayer.position.y <= tRock.position.y)
        {
            yield return new WaitForSeconds(timer);
            Instantiate(debris, new Vector3(tRock.position.x + offset, tRock.position.y, 0f), Quaternion.identity);
        }
    }

    private void Start()
    {
        tRock = GameObject.Find("rock").GetComponent<Transform>();
        tPlayer = GameObject.Find("player").GetComponent<Transform>();

        StartCoroutine(dropTimer());
    }

    
}
