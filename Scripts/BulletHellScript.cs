using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHellScript : MonoBehaviour
{
    [SerializeField] GameObject chopperBulletPrefab;
    [SerializeField] Transform bulletSpawn;
    private GameObject newBullet;
    public float RotA, RotB, rotSpeed, bulletDelay;
    public AudioSource gunshot;

    private IEnumerator Shooting()
    {
        yield return new WaitForSeconds(bulletDelay);
        if (newBullet == null)
        {
            gunshot.Play();
            newBullet = Instantiate(chopperBulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
        }
        StartCoroutine(Shooting());
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, rotSpeed);

        if (transform.eulerAngles.z < RotB || transform.eulerAngles.z > RotA) // rot.z > 45
        {
            rotSpeed = rotSpeed * -1;
        }

        if (newBullet != null && newBullet.active != true)
        {
            newBullet.SetActive(true);
            newBullet = null;
        }
    }

    public void StartFiring()
    {
        StartCoroutine(Shooting());
    }
}//EndScript