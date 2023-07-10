using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class labDroneCode : MonoBehaviour
{
    Transform Drone;
    public Transform playerModel;
    [SerializeField] Transform[] FlyPath;
    [SerializeField] float speed;
    private int position = 0;


    // Start is called before the first frame update
    void Start()
    {
        GameObject playerInScene = GameObject.Find("PlayerModel");
        Drone = transform;
        playerModel = playerInScene.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Drone.position == FlyPath[position].position)
        {
            if (position < FlyPath.Length - 1)
            {
                position++;
            } else
                {
                    position = 0;
                }
        } else
            {
                // sets the position vector to the next movement vector calculated by Vector3.MoveTowards()
                Drone.position = Vector3.MoveTowards(Drone.position, FlyPath[position].position, speed * Time.deltaTime);
            }
    }

    private void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (collision2D.transform == playerModel)
        {
            playerModel.parent = Drone;
        }
    }

    private void OnCollisionExit2D(Collision2D collision2D)
    {
        if (collision2D.transform == playerModel)
        {
            playerModel.parent = null;
        }
    }
}//EndScript