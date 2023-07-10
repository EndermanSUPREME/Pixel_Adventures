using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalBossScript : MonoBehaviour
{
    public int TotalHealth, Damage;
    [SerializeField] float osculatingSpeed;
    [SerializeField] Transform HighPos, LowPos, ElevationPoint, BossParent;
    [SerializeField] GameObject[] healthMarkerPrefab;
    SpriteRenderer spriteRenderer;
    [SerializeField] Sprite regularFace, partiallyDamaged, veryDamaged;
    GameObject PlayerModel, platformerStorage;
    [SerializeField] GameObject deathParticles;
    bool ChangeDir = false, EndTriggered = false;
    Animator headAnim;
    public AudioSource deathSound, injuryBoss;

    void Start()
    {
        deathParticles.SetActive(false);
        PlayerModel = GameObject.Find("PlayerModel");
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = regularFace;
        platformerStorage = GameObject.Find("infinitePlatforms");
        headAnim = GetComponent<Animator>();
    }

    private IEnumerator CreditDelay()
    {
        yield return new WaitForSeconds(7.5f);
        CreditsScene();
    }

    void Update()
    {
        if (TotalHealth > 0)
        {
            BossParent.position = Vector2.MoveTowards(BossParent.position, ElevationPoint.position, 1.15f * Time.deltaTime);

            if (TotalHealth == 40)
            {
                healthMarkerPrefab[4].SetActive(false);
            } else if (TotalHealth == 30)
                {
                    healthMarkerPrefab[3].SetActive(false);
                    spriteRenderer.sprite = partiallyDamaged;
                    Damage = 2;
                } else if (TotalHealth == 20)
                    {
                        healthMarkerPrefab[2].SetActive(false);
                    } else if (TotalHealth == 10)
                        {
                            healthMarkerPrefab[1].SetActive(false);
                            spriteRenderer.sprite = veryDamaged;
                            Damage = 4;
                        } else if (TotalHealth <= 0)
                            {
                                healthMarkerPrefab[0].SetActive(false);
                            }


            if (ChangeDir) // true : up
            {
                transform.position = Vector2.MoveTowards(transform.position, HighPos.position, osculatingSpeed * Time.deltaTime);
            } else // false : down
                {
                    transform.position = Vector2.MoveTowards(transform.position, LowPos.position, osculatingSpeed * Time.deltaTime);
                }

            if (transform.position == LowPos.position)
            {
                ChangeDir = true;
            } else if (transform.position == HighPos.position)
                {
                    ChangeDir = false;
                }
        } else
            {
                healthMarkerPrefab[0].SetActive(false);
                
                if (!EndTriggered)
                {
                    EndGame();
                    EndTriggered = true;
                }
            }
    }

    public void DamageEntities()
    {
        PlayerModel.transform.GetComponent<PlayerHealthScript>().TakeDamage(Damage);
    }

    public void TakeDamage(int amount)
    {
        if (TotalHealth > 0)
        {
            injuryBoss.Play();
            TotalHealth -= amount;
        }
    }

    private void EndGame()
    {
        // boss plays death animation
        deathSound.Play();
        headAnim.Play("FinalBossDeath");
        deathParticles.SetActive(true);
        deathParticles.transform.position = transform.position;

        // stops all current platforms
        Transform[] platformsActive = platformerStorage.transform.GetComponentsInChildren<Transform>();

        foreach (Transform platform in platformsActive)
        {
            if (platform.GetComponent<InfinitePlatformScript>() != null)
            {
                platform.GetComponent<InfinitePlatformScript>().enabled = false;
                Destroy(platform.gameObject.GetComponent<Rigidbody2D>());
            }
        }

        // stops the player when they're grounded incase they kill the boss while in the air or jumping to another platform
        if (PlayerModel.transform.GetComponent<playerMovement>() != null)
        {
            if (PlayerModel.transform.GetComponent<playerMovement>().IsGrounded() == true)
            {
                PlayerModel.transform.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                PlayerModel.transform.GetComponent<playerMovement>().enabled = false;
            }
        }

        StartCoroutine(CreditDelay());
    }

    private void CreditsScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}//EndScript