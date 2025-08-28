using System.Collections;
using UnityEngine;

public class spawner : MonoBehaviour
{
    [SerializeField] private Transform end;
    [SerializeField] private Transform tPlayer;

    [SerializeField] private float timer;
    [SerializeField] private float offset;

    [SerializeField] private GameObject asteroid;

    [SerializeField] private bool positionReached;

    private float spawnPos;


    private IEnumerator dropTimer()
    {
        while (tPlayer.position.y <= end.position.y)
        {
            yield return new WaitForSeconds(timer);
            Instantiate(asteroid, new Vector3(randomSpawn(), end.position.y, 0f), Quaternion.identity);
        }
    }
    
    private void Start()
    {
        StartCoroutine(dropTimer());    
    }

    private float randomSpawn()
    {
        spawnPos = Random.Range(end.position.x, end.position.x + offset);

        return spawnPos;
    }

}
