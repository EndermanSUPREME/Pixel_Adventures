using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShadowInteractions : MonoBehaviour
{
    public Transform playerObject; // base object
    public Sprite playerLight, playerDark; // appearences of player
    private SpriteRenderer playerRenderer;

    void Start()
    {
        GameObject player = GameObject.Find("PlayerModel");
        playerObject = player.transform;
        playerRenderer = playerObject.GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.transform == playerObject)
        {
            playerObject.GetComponent<playerMovement>().hidden = false;
            playerRenderer.sprite = playerLight;
        }
    }

    private void OnTriggerExit2D(Collider2D collider2D)
    {
        if (collider2D.transform == playerObject)
        {
            playerObject.GetComponent<playerMovement>().hidden = true;
            playerRenderer.sprite = playerDark;
        }
    }
}//EndScript