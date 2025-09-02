using System.Collections;
using UnityEngine;

public class destroySelf : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(destroySelfAfter());
    }

    private IEnumerator destroySelfAfter()
    {
        yield return new WaitForSeconds(6f);
        
        Destroy(gameObject);
    }
}
