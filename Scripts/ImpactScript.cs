using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactScript : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(ImpactLifeTime());
    }

    private IEnumerator ImpactLifeTime()
    {
        yield return new WaitForSeconds(0.45f);
        Destroy(gameObject);
    }
}//EndScript