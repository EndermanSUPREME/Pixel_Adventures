using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossBound : MonoBehaviour
{
    float truePlayerSpeed, checkSpeed;

    void Start()
    {
        truePlayerSpeed = GetComponent<playerMovement>().GetSpeed();
    }

    // Update is called once per frame
    void Update()
    {
        checkSpeed = GetComponent<playerMovement>().GetSpeed();
        // hard coded bounds
        if (transform.position.x <= -16 || transform.position.x >= 16)
        {
            GetComponent<playerMovement>().StopPlayer();
        }
        // allows player to get back in play-area
        if (transform.position.x <= -16 && checkSpeed == 0)
        {
            bool D = Input.GetKey(KeyCode. D);
            if (D)
            {
                transform.position = new Vector3(-15.95f, transform.position.y, 0);
                GetComponent<playerMovement>().ResetSpeed(truePlayerSpeed);
            }
        } else if (transform.position.x >= 16 && checkSpeed == 0)
            {
                bool A = Input.GetKey(KeyCode. A);
                if (A)
                {
                    transform.position = new Vector3(15.95f, transform.position.y, 0);
                    GetComponent<playerMovement>().ResetSpeed(truePlayerSpeed);
                }
            }
    }
}//EndScript