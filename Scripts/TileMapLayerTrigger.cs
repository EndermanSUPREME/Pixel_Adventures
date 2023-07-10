using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapLayerTrigger : MonoBehaviour
{
    GameObject player;
    TilemapRenderer tr;

    void Start()
    {
        player = GameObject.Find("PlayerModel");
        tr = transform.GetComponent<TilemapRenderer>();
    }

    private void OnTriggerStay2D(Collider2D collider2D)
    {
        if (collider2D.transform.gameObject == player)
        {
            tr.enabled = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collider2D)
    {
        if (collider2D.transform.gameObject == player)
        {
            tr.enabled = true;
        }
    }
}//EndScript