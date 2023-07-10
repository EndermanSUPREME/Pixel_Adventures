using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    private Rigidbody2D rigidbody2D;
    [SerializeField] Transform newCursor, throwRotation, throwSpawn;
    public Transform foot;
    public AudioSource leftFoot, rightFoot, jump, landing, knifeThrow;
    [SerializeField] GameObject UI_Darken, knifePrefab, newKnife;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Vector3 boxSize;
    [SerializeField] float speedConst, speed, jumpHeight, radius, angle, ThrowDelay;
    protected bool toggle, Throwing;
    protected bool a, d, Space, grounded, LeftShift;
    protected Animator playerAnimator;
    public bool hidden;
    Vector3 spawnPos;
    bool hover = true, hardLanding = false;

    // Start is called before the first frame update
    void Start()
    {
        spawnPos = transform.position;
        StartCoroutine(spawnDelay());
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
        GameObject gameCursor = GameObject.Find("Cursor");
        newCursor = gameCursor.transform;
        rigidbody2D = transform.GetComponent<Rigidbody2D>();
        playerAnimator = transform.GetComponent<Animator>();
        toggle = false;
        Throwing = false;
        speed = speedConst;
    }

    IEnumerator spawnDelay()
    {
        yield return new WaitForSeconds(1f);
        hover = false;
    }

    private void LeftStep()
    {
        leftFoot.Play();
    }

    private void RightStep()
    {
        rightFoot.Play();
    }

    // Update is called once per frame
    void Update()
    {
        Application.targetFrameRate = 60;
        
        if (!hover)
        {
            Movement();
            Weapons();
            ThrowingKnife();
        } else
            {
                transform.position = spawnPos;
                rigidbody2D.velocity = new Vector2(0, 0);
            }
    }

    private void Movement()
    {
        a = Input.GetKey(KeyCode. A);
        d = Input.GetKey(KeyCode. D);
        //         Physics2D.OverlapBox(center of the box, Vector3 of the box size, degree, layer)
        grounded = Physics2D.OverlapBox(foot.position, boxSize, 0, groundLayer);
        Space = Input.GetKeyDown(KeyCode. Space);
        LeftShift = Input.GetKey(KeyCode. LeftShift);

        if (LeftShift)
        {
            speed = speedConst * 2.5f;
        } else
            {
                speed = speedConst;
            }

        if (d)
        {
            rigidbody2D.velocity = new Vector2(speed, rigidbody2D.velocity.y);
        } else if (a)
            {
                rigidbody2D.velocity = new Vector2(-speed, rigidbody2D.velocity.y);
            } else
                {
                    rigidbody2D.velocity = new Vector2(0, rigidbody2D.velocity.y);
                };

        if (rigidbody2D.velocity.y < -8.25f)
        {
            hardLanding = true;
        }

        if (grounded && hardLanding)
        {
            landing.Play();
            hardLanding = false;
        }

        if (grounded == true && Space == true)
        {
            rigidbody2D.AddForce(transform.up * jumpHeight, ForceMode2D.Impulse);
            jump.Play();
        }

        AnimatingCharacter();
    }

    private void AnimatingCharacter()
    {
        float X;

        X = Input.GetAxis("Horizontal");

        if (hidden == false)
        {
            if (a == false && d == false)
            {
                if (X > 0)
                {
                    playerAnimator.Play("IdleRight");
                } else if (X < 0)
                    {
                        playerAnimator.Play("IdleLeft");
                    }
            } else if (a == true || d == true)
                {
                    if (X > 0)
                    {
                        playerAnimator.Play("walkRight");
                    } else if (X < 0)
                        {
                            playerAnimator.Play("walkLeft");
                        }
                }
        } else
            {
                if (a == false && d == false)
                {
                    if (X > 0)
                    {
                        playerAnimator.Play("DarkIdleRight");
                    } else if (X < 0)
                        {
                            playerAnimator.Play("DarkIdleLeft");
                        }
                } else if (a == true || d == true)
                    {
                        if (X > 0)
                        {
                            playerAnimator.Play("DarkWalkRight");
                        } else if (X < 0)
                            {
                                playerAnimator.Play("DarkWalkLeft");
                            }
                    }
            }
    }

    private void Weapons()
    {
        bool equipThrowKnife = Input.GetKeyDown(KeyCode. Alpha1);

        if (equipThrowKnife && toggle == false)
        {
            toggle = true;
        } else if (equipThrowKnife && toggle == true)
            {
                toggle = false;
            }

        if (toggle == true)
        {
            newCursor.gameObject.SetActive(true);
            ConfigCursor();
            UI_Darken.SetActive(false);
        } else
            {
                newCursor.gameObject.SetActive(false);
                UI_Darken.SetActive(true);
            }
    }

    // sets the newCursor to the position of the mouse on screen
    private void ConfigCursor()
    {
        Vector3 MousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        newCursor.position = new Vector3(MousePosition.x, MousePosition.y, 0);

        AngleThrow();
    }

    // moves the player in direction of cursor
    private void AngleThrow()
    {
        Vector2 mousePos = Input.mousePosition;

        float X = throwRotation.position.x - newCursor.position.x;
        float Y = throwRotation.position.y - newCursor.position.y;

        angle = Mathf.Atan2(Y, X) * Mathf.Rad2Deg;
        
        throwRotation.eulerAngles = new Vector3(0, 0, angle + 180);

        bool Fire1 = Input.GetButtonDown("Fire1");
        if (Fire1 && Throwing == false)
        {
            if (newKnife == null)
            {
                newKnife = Instantiate(knifePrefab, throwSpawn.position, throwSpawn.rotation);
                knifeThrow.Play();
                Throwing = true;
                StartCoroutine(ThrowingDelay());
            }
        }
    }

    private IEnumerator ThrowingDelay()
    {
        yield return new WaitForSeconds(ThrowDelay);
        Throwing = false;
    }

    private void ThrowingKnife()
    {
        if (newKnife != null)
        {
            if (newKnife.active == false)
            {   
                newKnife.SetActive(true);
            } else
                {
                    newKnife = null;
                }
        }
    }

    public void StopPlayer()
    {
        speedConst = 0;
    }

    public float GetSpeed()
    {
        return speedConst;
    }

    public void ResetSpeed(float keptspeed)
    {
        speedConst = keptspeed;
    }

    public bool IsGrounded()
    {
        return grounded;
    }

    private void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (collision2D.transform.GetComponent<InfinitePlatformScript>() != null)
        {
            transform.parent = collision2D.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision2D)
    {
        if (collision2D.transform.GetComponent<InfinitePlatformScript>() != null)
        {
            transform.parent = null;
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Draw a square to visualize the OverlapBox
        Gizmos.color = Color.red;
        Gizmos.DrawCube(foot.position, boxSize);
    }
}//EndScript