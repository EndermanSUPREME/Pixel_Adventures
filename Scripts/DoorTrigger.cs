using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    [SerializeField] Transform door;
    public SpriteRenderer renderer;
    [SerializeField] Sprite boxOn, boxOff;
    private bool unlocked = false;

    void Start()
    {
        renderer.sprite = boxOn;
    }

    void Update()
    {
        if (unlocked)
        {
            door.position = new Vector3(door.position.x - 0.05f, door.position.y, door.position.z);
        }
    }

    void OnCollisionEnter2D(Collision2D collision2D)
    {
        renderer.sprite = boxOff;
        unlocked = true;
    }
}//EndScript