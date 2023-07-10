using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boulderScript : MonoBehaviour
{
    Rigidbody2D rb2D;
    [SerializeField] GameObject Player;
    [SerializeField] float speed;
    public int Direction = 1;
    private bool inAir = true;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = transform.GetComponent<Rigidbody2D>();
        Player = GameObject.Find("PlayerModel");
        StartCoroutine(LifeTime());
    }

    private IEnumerator LifeTime()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (inAir)
        {
            rb2D.velocity = new Vector2(speed * Direction, 0);
        };
    }

    private void OnCollisionEnter2D(Collision2D collision2D)
    {
        inAir = false;
        
        if (Player != null && collision2D.transform == Player.transform)
        {
            Player.GetComponent<PlayerHealthScript>().TakeDamage(1);
            Destroy(gameObject);
        }
    }
}//EndScript