using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laserBeamScript : MonoBehaviour
{
    public Transform Player;
    AudioSource laserSound;

    void Start()
    {
        GameObject playerInScene = GameObject.Find("PlayerModel");
        laserSound = GetComponent<AudioSource>();
        Player = playerInScene.transform;
    }

    private IEnumerator KnockDown()
    {
        yield return new WaitForSeconds(1);
        Player.GetComponent<playerMovement>().enabled = true;
    }

    void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (collision2D.transform == Player)
        {
            laserSound.Play();

            Player.GetComponent<PlayerHealthScript>().TakeDamage(1);

            if (Player.GetComponent<Rigidbody2D>() != null)
            {
                if (Player.position.x > transform.position.x)
                {
                    Player.GetComponent<playerMovement>().enabled = false;
                    StartCoroutine(KnockDown());
                    Player.GetComponent<Rigidbody2D>().AddForce(transform.right * 2, ForceMode2D.Impulse);
                }

                if (Player.position.x < transform.position.x)
                {
                    Player.GetComponent<playerMovement>().enabled = false;
                    StartCoroutine(KnockDown());
                    Player.GetComponent<Rigidbody2D>().AddForce(transform.right * -2, ForceMode2D.Impulse);
                }
            }
        }
    }
}//EndScript