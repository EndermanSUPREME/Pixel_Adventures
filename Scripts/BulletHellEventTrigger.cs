using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHellEventTrigger : MonoBehaviour
{
    public Animator chopper;
    public Transform chopperTrans;
    public Transform[] chopperGuns;
    public GameObject leftBorder, rightBorder;
    private bool gunsFiring = false;

    void Start()
    {
        leftBorder.SetActive(false);
        rightBorder.SetActive(false);
    }

    void Update()
    {
        if (gunsFiring == false && chopperTrans.position.y <= 4.5f)
        {
            ChopperFight();
            gunsFiring = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.transform.tag == "Player")
        {
            leftBorder.SetActive(true);
            rightBorder.SetActive(true);
            transform.position = new Vector3(transform.position.x, 15, transform.position.z);
            chopper.Play("initial");
        }
    }

    private void ChopperFight()
    {
        for (int i = 0; i < chopperGuns.Length; i++)
        {
            chopperGuns[i].GetComponent<BulletHellScript>().StartFiring();
        }
    }
}//EndScript