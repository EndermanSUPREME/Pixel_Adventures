using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2_audio : MonoBehaviour
{
    GameObject Player;
    public GameObject clubMusic, outdoors;
    public bool inside = false;

    void Start()
    {
        Player = GameObject.Find("PlayerModel");

        clubMusic.SetActive(false);
        outdoors.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.transform.gameObject == Player)
        {
            if (inside)
            {
                clubMusic.SetActive(true);
                outdoors.SetActive(false);
                inside = false;
            } else
                {
                    clubMusic.SetActive(false);
                    outdoors.SetActive(true);
                    inside = true;
                }
        }
    }
}//EndScript