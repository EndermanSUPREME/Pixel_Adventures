using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingPlatformScript : MonoBehaviour
{
    public Transform Player;
    private bool OnPlatform = false;

    void Update()
    {
        bool Space = Input.GetKeyDown(KeyCode. Space);

        if (Space && OnPlatform == true)
        {
            Player.parent = null;
        }
    }

    void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (collision2D.transform == Player)
        {
            Player.SetParent(transform);
            OnPlatform = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision2D)
    {
        if (collision2D.transform == Player)
        {
            Player.parent = null;
            OnPlatform = false;
        }
    }
}//EndScript