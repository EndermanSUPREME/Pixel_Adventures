using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHell_EndScript : MonoBehaviour
{
    [SerializeField] GameObject Chopper;
    [SerializeField] GameObject[] BulletHellArea;

    // Update is called once per frame
    void Update()
    {
        if (Chopper == null)
        {
            for (int i = 0; i < BulletHellArea.Length; i++)
            {
                BulletHellArea[i].SetActive(false);
            }
        }
    }
}//EndScript