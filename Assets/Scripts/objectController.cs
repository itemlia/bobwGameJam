using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class objectController : MonoBehaviour
{
    [SerializeField] private Transform tRock;
    [SerializeField] private Transform tPlayer;

    [SerializeField] private float timer;
    [SerializeField] private float offset;

    [SerializeField] private GameObject debris;



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
