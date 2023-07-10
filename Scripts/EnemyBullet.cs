using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D bulletRB;
    [SerializeField] private float maxDistance, currentDist;
    public float speed;
    public int damage = 1;
    [SerializeField] private Transform Enemy, playerModel;
    public GameObject ImpactLeftPrefab;
    GameObject newImpact;

    void Start()
    {
        GameObject playerInScene = GameObject.Find("PlayerModel");
        playerModel = playerInScene.transform;

        StartCoroutine(lifeExpectancy());
    }

    private IEnumerator lifeExpectancy()
    {
        yield return new WaitForSeconds(10);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        bulletRB.velocity = transform.right * speed;

        currentDist = Vector2.Distance(transform.position, Enemy.position);

        if (Enemy == null)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (collision2D.transform == playerModel)
        {
            // player Death
            playerModel.transform.GetComponent<PlayerHealthScript>().TakeDamage(damage);
        }

        if (newImpact == null)
        {
            newImpact = Instantiate(ImpactLeftPrefab, transform.position, ImpactLeftPrefab.transform.rotation);
            Destroy(gameObject);
        }
    }
}//EndScript