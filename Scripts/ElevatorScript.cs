using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorScript : MonoBehaviour
{
    public Transform LowPoint, HighPoint;
    public Transform PlayerModel;
    public float speed;
    public GameObject PopUp;
    private bool moving = false, delay = true;
    public bool up = true;

    void Start()
    {
        GameObject player = GameObject.Find("PlayerModel");
        PlayerModel = player.transform;
        // PopUp = GameObject.Find("PopUp");

        if (PopUp != null)
        {
            PopUp.SetActive(false);
        }
    }

    void Update()
    {
        float distance = Vector2.Distance(PlayerModel.position, transform.position);
        if (distance <= 1)
        {
            ElevatorFunction();
        }
    }

    private IEnumerator ElevatorDelay()
    {
        yield return new WaitForSeconds(0.5f);
        delay = false;
    }

    private void ElevatorFunction()
    {
        bool E = Input.GetKeyDown(KeyCode. E);
        if (E && (up == true || up == false) && moving == false)
        {
            moving = true;
            StartCoroutine(ElevatorDelay());
        }

        if (moving == true)
        {
            switch (up)
            {
                case true:
                    transform.Translate(transform.up * speed * Time.deltaTime);
                    delay = true;
                break;
                case false:
                    transform.Translate(-transform.up * speed * Time.deltaTime);
                    delay = true;
                break;
                default:
                break;
            }
        }

        if (moving == true && (transform.position.y == LowPoint.transform.position.y || transform.position.y == HighPoint.transform.position.y) && delay == false)
        {
            moving = false;
        }

        if (transform.position.y <= LowPoint.transform.position.y)
        {
            transform.position = new Vector3(transform.position.x, LowPoint.transform.position.y, transform.position.z);
            moving = false;
            up = true;
        } else if (transform.position.y >= HighPoint.transform.position.y)
            {
                transform.position = new Vector3(transform.position.x, HighPoint.transform.position.y, transform.position.z);
                moving = false;
                up = false;
            }
    } // end of elevatorFunction void

//==============================================================================

    private void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (collision2D.transform == PlayerModel)
        {
            PlayerModel.SetParent(transform);
        }
    }

    private void OnCollisionStay2D(Collision2D collision2D)
    {
        if (collision2D.transform == PlayerModel)
        {
            PopUp.SetActive(true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision2D)
    {
        if (collision2D.transform == PlayerModel)
        {
            PlayerModel.parent = null;
        }

        PopUp.SetActive(false);
    }
}//EndScript