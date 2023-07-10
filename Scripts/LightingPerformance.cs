using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal; // light2d lib

public class LightingPerformance : MonoBehaviour
{
    public Transform[] lights;
    [SerializeField] [Range(0, 25)] int range;

    void FixedUpdate()
    {
        for (int i = 0; i < lights.Length; i++)
        {
            if (Vector2.Distance(transform.position, lights[i].position) <= range)
            {
                lights[i].GetComponent<Light2D>().enabled = true;
            } else
                {
                    lights[i].GetComponent<Light2D>().enabled = false;
                }
        }
    }
}//EndScript