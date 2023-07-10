using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossHandsScript : MonoBehaviour
{
    [SerializeField] Transform FinalBoss;
    GameObject PlayerModel;
    Animator anim;
    float Time;
    [SerializeField] float impactRadius;
    [SerializeField] GameObject ImpactParticles;
    public AudioSource groundPound;
    bool contactMade = false;

    private IEnumerator AttackDelay(float time)
    {
        yield return new WaitForSeconds(time);
        anim.SetBool("attack", true);
        Time = Random.Range(1.5f,3);
        StartCoroutine(AttackDelay(Time));
    }

    void Start()
    {
        anim = GetComponent<Animator>();
        PlayerModel = GameObject.Find("PlayerModel");

        Time = Random.Range(1,3);
        StartCoroutine(AttackDelay(Time));
    }

    public void AttackFinished()
    {
        anim.SetBool("attack", false);
        contactMade = false;
        anim.SetBool("attack", false);
    }

    private void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (!contactMade && collision2D.transform.GetComponent<ThrowingKnife>() == null && anim.GetBool("attack")) // ensures the particles dont spawn when a throwing knife hits the hand or if the hand touches a platform
        {
            contactMade = true;

            groundPound.Play();
            
            // gets the first point of collision
            ContactPoint2D poc = collision2D.contacts[0];
            // spawn particle system at point of impact
            GameObject newImpactParticles = Instantiate(ImpactParticles, poc.point, ImpactParticles.transform.rotation);
            // spawns a overlappingcircle at the point of collision with a set radius
            Collider2D[] colliders2D = Physics2D.OverlapCircleAll(poc.point, impactRadius);
            // scanes any colliders detected for a player
            for (int i = 0; i < colliders2D.Length; i++)
            {
                // player takes damage from being in the radius of the impact
                if (colliders2D[i].transform == PlayerModel.transform)
                {
                    FinalBoss.GetComponent<FinalBossScript>().DamageEntities();
                }
            }
        }
    }
}//EndScript