using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveBeatleScript : MonoBehaviour
{
    [SerializeField] Transform DestinationOne, DestinationTwo, playerModel, Eye, spitSpawn;
    [SerializeField] GameObject spitPrefab;
    private GameObject newSpitPrefab;
    [SerializeField] float speed, timer, eyeSight, detectionNumber = 0;
    public int timesTurnedAround = 0;
    bool changeDir = true, walking = true, playerSeen = false, reset = false, attackPlayer = false;
    Animator anim;
    Vector2 lastPos;
    RaycastHit2D sight;
    public AudioSource acidSpit;

    void Start()
    {
        anim = transform.GetComponent<Animator>();
    }

    private IEnumerator StandStill()
    {
        yield return new WaitForSeconds(timer);

        walking = true;
    }

    private IEnumerator LookAround()
    {
        yield return new WaitForSeconds(2.55f);

        AI_TurnAround();
        StartCoroutine(LookAround());
    }

    private IEnumerator LoseInterest()
    {
        yield return new WaitForSeconds(1.5f);

        detectionNumber -= 2;
        StartCoroutine(LoseInterest());
    }

    void Update()
    {
        if (attackPlayer == false)
        {
            AI_Movement();
            AnimatingCharacter();
            looking();
        } else // true to attack
            {
                AttackingPlayer();
            };

        if (detectionNumber < 0)
        {
            detectionNumber = 0;
        }
    }

    private void AI_Movement()
    {
        if (walking == true) // switches when the ai moves on or past its destination point
        {
            transform.GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0);
            
            if (detectionNumber >= 35)
            {
                if (playerSeen == true) // sets the last seen point of the player
                {
                    lastPos = new Vector2(playerModel.position.x, DestinationOne.position.y);
                    reset = false;
                    playerSeen = false;
                }
// This copy and pasted block is edited to move towards the lastPos vector2 value

                    if (changeDir == false) // ai is moving left to lastPos.x
                    {
                        speed = (Mathf.Abs(speed)) * -1;
                        if (transform.position.x <= lastPos.x)
                        {
                            changeDir = true;
                            walking = false;
                            timer = Random.Range(3.65f, 5.75f);
                            StartCoroutine(LookAround());
                        }
                    } else // ai is moving right to lastPos.x
                        {
                            speed = Mathf.Abs(speed);
                            if (transform.position.x >= lastPos.x)
                            {
                                changeDir = false;
                                walking = false;
                                timer = Random.Range(3.65f, 5.75f);
                                StartCoroutine(LookAround());
                            }
                        }
            } else // ai hasnt seen player long enough to worry about it
                { // this block BELOW is regular path patterns
                
                    StopCoroutine(LookAround());

                    if (changeDir == false) // ai is moving left
                    {
                        speed = (Mathf.Abs(speed)) * -1;
                        if (transform.position.x <= DestinationOne.position.x)
                        {
                            changeDir = true;
                            walking = false;
                            timer = Random.Range(0.5f, 2.5f);
                            StartCoroutine(StandStill());
                        }
                    } else // ai is moving right
                        {
                            speed = Mathf.Abs(speed);
                            if (transform.position.x >= DestinationTwo.position.x)
                            {
                                changeDir = false;
                                walking = false;
                                timer = Random.Range(0.5f, 2.5f);
                                StartCoroutine(StandStill());
                            }
                        }
                }

        } else // ai stands still and look in the last direction is was last moving
            {
                transform.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            }
    }

    private void AI_TurnAround()
    {
        if (timesTurnedAround < 4)
        {
            timesTurnedAround++;
            speed = -speed;
        }

        // if (timesTurnedAround > 3 && sight.transform != playerModel)
        // {
        //     StartCoroutine(LoseInterest());
        // }
    }

    private void AnimatingCharacter()
    {
        if (transform.GetComponent<Rigidbody2D>().velocity.x == 0) // ai isnt moving
        {
            if (speed > 0) // right idle
            {
                anim.Play("");
                transform.eulerAngles = new Vector3(0, 180, 0);
            } else if (speed < 0) // left idle
                {
                    anim.Play("");
                    transform.eulerAngles = new Vector3(0, 0, 0);
                }
        } else
            {
                if (speed > 0) // moving right
                {
                    anim.Play("beatleWalk");
                    transform.eulerAngles = new Vector3(0, 180, 0);
                } else if (speed < 0) // moving left
                    {
                        anim.Play("beatleWalk");
                        transform.eulerAngles = new Vector3(0, 0, 0);
                    }
            }
    }
//==================== Assist eye ==================
    private void looking()
    {
        if (speed > 0) // look right
        {
            Eye.transform.eulerAngles = new Vector3(0, 0, 90);
        } else if (speed < 0) // look left
            {
                Eye.transform.eulerAngles = new Vector3(0, 0, -90);
            }
    }
//===================== detected the player in view angle =========================
    public void Detect()
    {
        //print("Seen");
        float DistX = Mathf.Abs(transform.position.x - playerModel.transform.position.x);

        if (DistX >= 3.25f)
        {
            detectionNumber++;
        } else
            {
                detectionNumber = detectionNumber + (2/DistX);
            }

            StopCoroutine(LookAround());
            StopCoroutine(LoseInterest());
            timesTurnedAround = 0;
            playerSeen = true;

        if (detectionNumber < 0)
        {
            detectionNumber = 0;
            StopCoroutine(LookAround());
            StopCoroutine(LoseInterest());
        }

        if (detectionNumber <= 25) // reverts to its normal
        {
            if (reset == false)
            {
                walking = true;
                reset = true;
            }

            StopCoroutine(LookAround());
            StopCoroutine(LoseInterest());
            timesTurnedAround = 0;
        }

        if (detectionNumber >= 75)
        {
            // attacking player
            StopAllCoroutines();

            attackPlayer = true;
        }
    }

    //============== Attacking =================//

    private void AttackingPlayer()
    {
        float distance = Vector2.Distance(transform.position, playerModel.position);

        if (distance > 15 && attackPlayer)
        {
            StartCoroutine(LoseInterest());

            if (detectionNumber < 0)
            {
                StopAllCoroutines();
                detectionNumber = 0;
                attackPlayer = false;
            }
        }

        if (newSpitPrefab == null)
        {
            if (playerModel.position.x < transform.position.x)
            {
                // attack animation left
                    transform.eulerAngles = new Vector3(0, 0, 0);
                    anim.Play("beatleAttack");
            } else if (playerModel.position.x > transform.position.x)
                {
                    // attack animation right
                    transform.eulerAngles = new Vector3(0, 180, 0);
                    anim.Play("beatleAttack");
                }
        }
    }

    private void SpitAcid() // animation event
    {
        if (newSpitPrefab == null)
        {
            acidSpit.Play();
            newSpitPrefab = Instantiate(spitPrefab, spitSpawn.position, spitPrefab.transform.rotation);

            if (transform.eulerAngles.y == 0)
            {
                newSpitPrefab.GetComponent<beatleSpitScript>().Direction = -1;
            } else if (transform.eulerAngles.y == 180)
                {
                    newSpitPrefab.GetComponent<beatleSpitScript>().Direction = 1;
                }
        };
    }

    private void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (collision2D.transform == playerModel)
        {
            detectionNumber = 110;
            attackPlayer = true;
        }
    }

}//EndScript