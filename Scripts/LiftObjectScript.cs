using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftObjectScript : MonoBehaviour
{
    public bool HorizontalDirection;
    [SerializeField] Transform PointA, PointB, LiftObj;
    bool changeDir = false;
    public float movementSpeed = 0.25f;

    // Update is called once per frame
    void Update()
    {
        if (HorizontalDirection)
        {
            if (changeDir == true)
            {
                if (LiftObj.position.x > PointB.position.x)
                {
                    changeDir = false;
                }
                LiftObj.position = new Vector3(LiftObj.position.x + movementSpeed, LiftObj.position.y, LiftObj.position.z);
            } else
                {
                    if (LiftObj.position.x < PointA.position.x)
                    {
                        changeDir = true;
                    }
                    LiftObj.position = new Vector3(LiftObj.position.x - movementSpeed, LiftObj.position.y, LiftObj.position.z);
                }
        } else // not horizontal movement
            {
                if (changeDir == true)
                {
                    if (LiftObj.position.y > PointB.position.y)
                    {
                        changeDir = false;
                    }
                        LiftObj.position = new Vector3(LiftObj.position.x, LiftObj.position.y + movementSpeed, LiftObj.position.z);
                } else
                    {
                        if (LiftObj.position.y < PointA.position.y)
                        {
                            changeDir = true;
                        }
                        LiftObj.position = new Vector3(LiftObj.position.x, LiftObj.position.y - movementSpeed, LiftObj.position.z);
                    }
            }
    }
}//EndScript