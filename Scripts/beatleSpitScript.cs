using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class beatleSpitScript : MonoBehaviour
{
    Rigidbody2D rb2D;
    SpriteRenderer spriteRenderer;
    [SerializeField] GameObject Player;
    [SerializeField] Sprite spitProjectile, spitSplatter;
    [SerializeField] float speed;
    public int Direction = 1;
    private bool inAir = true, initialBurn = false;
    public AudioSource acidBurn;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = transform.GetComponent<Rigidbody2D>();
        spriteRenderer = transform.GetComponent<SpriteRenderer>();
        StartCoroutine(LifeTime(6));
    }

    private IEnumerator LifeTime(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }

    private IEnumerator AcidDamage()
    {
        yield return new WaitForSeconds(2f);
        acidBurn.Play();
        Player.transform.GetComponent<PlayerHealthScript>().TakeDamage(1);
        StartCoroutine(AcidDamage());
    }

    // Update is called once per frame
    void Update()
    {
        Player = GameObject.Find("PlayerModel");

        if (inAir) // true
        {
            spriteRenderer.sprite = spitProjectile;
            rb2D.velocity = new Vector2(speed * Direction, 0);
        } else // false
            {
                rb2D.velocity = new Vector2(0, 0);
                rb2D.gravityScale = 0;
            };
    }

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.transform == Player.transform && spriteRenderer.sprite == spitProjectile)
        {
            Player.GetComponent<PlayerHealthScript>().TakeDamage(1);
            Destroy(gameObject);
        } else
            {
                inAir = false;
                if (spriteRenderer.sprite != spitSplatter)
                {
                    BoxCollider2D boxCollider2D = transform.GetComponent<BoxCollider2D>();
                    boxCollider2D.size = new Vector2(0.5f, 0.08f);
                    boxCollider2D.offset = new Vector2(0.0007873774f, -0.2f);
                    transform.position = new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z);
                }

                spriteRenderer.sprite = spitSplatter;
                StartCoroutine(LifeTime(50));

                if (collider2D.transform == Player.transform)
                {
                    if (collider2D.transform == Player.transform && spriteRenderer.sprite == spitSplatter)
                    {
                        if (initialBurn == false)
                        {
                            Player.GetComponent<PlayerHealthScript>().TakeDamage(1);
                            initialBurn = true;
                        }
                    }

                    StartCoroutine(AcidDamage());
                }
            }
    }

    private void OnTriggerExit2D(Collider2D collider2D)
    {
        if (Player != null && collider2D.transform == Player.transform && spriteRenderer.sprite == spitSplatter)
        {
            StopCoroutine(AcidDamage());
            initialBurn = false;
        }
    }
}//EndScript