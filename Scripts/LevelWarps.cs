using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelWarps : MonoBehaviour
{
    public Transform[] points; // positions the player warps to
    public Transform player;
    private int index = 0;
    [SerializeField] private UnityEvent[] GameEvent;

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.transform == player)
        {
            if (GameEvent != null)
            {
                GameEvent[index].Invoke();
            }
            
            TeleportToSection();
        }
    }

    private void TeleportToSection()
    {
        player.transform.position = points[index].position;

        if (index < points.Length)
        {
            index++;
        }
    }
}//EndScript