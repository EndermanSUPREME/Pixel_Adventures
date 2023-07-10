using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingKnife : MonoBehaviour
{
    [SerializeField] private Rigidbody2D knifeRB;
    [SerializeField] private float speed, maxDistance, currentDist;
    public int damage = 1;
    [SerializeField] private Transform player;
    public GameObject ImpactLeftPrefab;
    GameObject newImpact;
    private Transform objectCollidedWith;

    // Update is called once per frame
    void Update()
    {
        knifeRB.velocity = transform.right * speed;
        
        currentDist = Vector2.Distance(transform.position, player.position);

        if (currentDist >= maxDistance)
        {
            Destroy(gameObject);
        }

        if (newImpact != null && newImpact.active == false)
        {
            newImpact.SetActive(true);
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision2D)
    {
        objectCollidedWith = collision2D.transform;

        if (newImpact == null)
        {
            newImpact = Instantiate(ImpactLeftPrefab, transform.position, ImpactLeftPrefab.transform.rotation);
        }

        if (objectCollidedWith.GetComponent<EnemyHealth>() != null)
        {
            // AI Death
            objectCollidedWith.GetComponent<EnemyHealth>().TakeDamage(damage);
        }

        if (objectCollidedWith.GetComponent<FinalBossScript>() != null)
        {
            // final boss
            objectCollidedWith.GetComponent<FinalBossScript>().TakeDamage(damage);
        }
    }
}//EndScript