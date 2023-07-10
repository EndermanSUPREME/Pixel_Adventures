using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyParallax : MonoBehaviour
{
    [SerializeField] Transform Base, parallaxLayer, Front;
    public int layerNum;
    public int layerCount;
    [SerializeField] Transform playerCamera;
    
    float dist, offset, newX;

    void Start()
    {
        playerCamera = Camera.main.transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        dist = Base.position.x - Front.position.x;
        offset = Mathf.Abs(dist/layerCount);

        Front.position = new Vector3(playerCamera.position.x, Front.position.y, 0);

        if (parallaxLayer != null)
        {
            if (dist < 0)
            {
                newX = Front.position.x - (offset * layerNum);
            } else
                {
                    newX = Front.position.x + (offset * layerNum);
                }
            parallaxLayer.position = new Vector3(newX, parallaxLayer.position.y, 0);
        }
    }
}//EndScript