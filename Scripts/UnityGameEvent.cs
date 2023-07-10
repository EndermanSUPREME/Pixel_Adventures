using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UnityGameEvent : MonoBehaviour
{
    [SerializeField] UnityEvent GameEvent;
    [SerializeField] Transform PlayerModel;

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.transform == PlayerModel)
        {
            GameEvent.Invoke();
        }
    }
}//EndScript