using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfinitePlatformScript : MonoBehaviour
{
    [SerializeField] GameObject[] platformPrefab;
    float speed = -0.65f;
    GameObject newPlatform, Player, platformerStorage;
    Rigidbody2D rigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        Player = GameObject.Find("PlayerModel");
        platformerStorage = GameObject.Find("infinitePlatforms");

        transform.parent = platformerStorage.transform;
    }

    // Update is called once per frame
    void Update()
    {
        rigidbody2D.velocity = new Vector2(speed, 0);

        float Dist = Mathf.Abs(Player.transform.position.x - transform.position.x), newX = transform.position.x + Random.Range(11, 14), newY;

        if (transform.position.y <= -10)
        {
            newY = transform.position.y + 1.5f;
        } else if (transform.position.y >= -5)
            {
                newY = transform.position.y + 1.5f;
            } else
                {
                    newY = transform.position.y + Random.Range(-1.35f, 1.35f);
                }

        if (Dist < 15 && newPlatform == null)
        {
            newPlatform = Instantiate(platformPrefab[Random.Range(0, platformPrefab.Length)], new Vector3(newX, newY, 0), Quaternion.identity);
        };

        if (transform.position.x <= -24)
        {
            Destroy(transform.gameObject);
        }
    }
}//EndScript