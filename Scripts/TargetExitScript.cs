using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetExitScript : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision2D)
    {
        print("Quit App");
        Application.Quit();
    }
}//EndScript