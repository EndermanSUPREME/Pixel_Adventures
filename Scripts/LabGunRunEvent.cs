using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabGunRunEvent : MonoBehaviour
{
    private bool EventRunning = false, attackPlayer = false, fireBullet = true, CamAnimBool = false, GunAnimBool = false;
    private GameObject newBullet;
    [SerializeField] GameObject LeftBoarder, StartTrigger, EndTrigger, EventCamera, GameCamera, bulletPrefab;
    [SerializeField] Transform PlayerModel, GunPivotPoint, bulletSpawn;
    [SerializeField] Animator largeGun, EventCam;
    [SerializeField] float shootingDelay;
    public AudioSource gunshot;

    void Start()
    {
        LeftBoarder.SetActive(false);
        EndTrigger.SetActive(false);
        EventCamera.SetActive(false);
        StartTrigger.SetActive(true);
    }

    private void StartEvent()
    {
        // Cameras are set to play the camera animation revealing the large gun
        GameCamera.SetActive(false);
        EventCamera.SetActive(true); // camera animation should play upon activation
        // borders and trigger objects are set for event to commence
        LeftBoarder.SetActive(true);
        EndTrigger.SetActive(true);
        StartTrigger.GetComponent<BoxCollider2D>().enabled = false;
    }

    private void GunSetUp() // triggered by event in event_camera animation
    {
        largeGun.Play("labGunInitial");
    }

    private void ReadyAimAtPlayer() // triggered by event in labGunInitial animation
    {
        GameCamera.SetActive(true);
        EventCamera.SetActive(false);
        attackPlayer = true;
    }

    async void Update()
    {
        if (attackPlayer == true)
        {
            ShootAtPlayer();
        } else
            {
                if (CamAnimBool == false && EventCam.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.8f && EventCamera.active == true)
                {
                    GunSetUp();
                    CamAnimBool = true;
                }

                if (GunAnimBool == false && largeGun.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.8f && largeGun.GetCurrentAnimatorStateInfo(0).IsName("labGunInitial"))
                {
                    ReadyAimAtPlayer();
                    GunAnimBool = true;
                }
            }
    }

    private IEnumerator FiringDelay()
    {
        yield return new WaitForSeconds(shootingDelay);
        fireBullet = true;
    }

    private void ShootAtPlayer()
    {
        GunPivotPoint.GetComponent<Animator>().enabled = false;
//==================== Seting Aim With Basic-Inverse-Trig ===========================
        float X = GunPivotPoint.position.x - PlayerModel.position.x;
        float Y = GunPivotPoint.position.y - PlayerModel.position.y;
        float RotZ = Mathf.Atan2(Y, X) * Mathf.Rad2Deg;

        GunPivotPoint.eulerAngles = new Vector3(0, 0, RotZ);
//==================== Creating new bullets and firing them at a chosen rate ========
        if (fireBullet == true) // perms to fire a new round
        {
            if (newBullet == null)
            {
                gunshot.Play();
                newBullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
            } else // bullet has been created
                {
                    if (newBullet.active == false)
                    {
                        newBullet.SetActive(true);
                    } else // bullet is active in scene
                        {
                            StartCoroutine(FiringDelay());
                            fireBullet = false;
                            newBullet = null;
                        }
                }
        }
    }

    public void EndEvent()
    {
        attackPlayer = false;

        GameCamera.SetActive(true);
        EventCamera.SetActive(false);
        
        LeftBoarder.SetActive(false);
        EndTrigger.SetActive(false);
        StartTrigger.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.transform == PlayerModel)
        {
            if (EventRunning == false)
            {
                EventRunning = true;
                StartEvent();
            } else
                {
                    EventRunning = false;
                    EndEvent();
                }
        }
    }
}//EndScript