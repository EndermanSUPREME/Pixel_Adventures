using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laserBox : MonoBehaviour
{
    [SerializeField] GameObject LaserObj;
    [SerializeField] Sprite BoxOn, BoxOff;
    public bool poweredOn;
    public AudioSource boxSound;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = BoxOn;
        poweredOn = true;
        LaserObj.SetActive(true);
    }

    void OnCollisionEnter2D(Collision2D collision2D)
    {
        boxSound.Play();
        GetComponent<SpriteRenderer>().sprite = BoxOff;
        LaserObj.SetActive(false);
        poweredOn = false;
    }
}//EndScript