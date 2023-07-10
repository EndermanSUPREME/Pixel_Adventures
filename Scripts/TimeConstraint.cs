using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimeConstraint : MonoBehaviour
{
    [SerializeField] float TimeLimit;
    public Text timerShow;
    GameObject PlayerModel;
    Transform player;
    bool completedTimeSegment = false;

    void Start()
    {
        PlayerModel = GameObject.Find("PlayerModel");
        player = PlayerModel.transform;
    }

    void Update()
    {
        if (completedTimeSegment)
        {
            Destroy(transform.gameObject);
        }
    }

    void FixedUpdate()
    {
        if (TimeLimit > 0)
        {
            TimeLimit = TimeLimit - 0.01f;
        } else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }

        timerShow.text = TimeLimit.ToString() + "sec";
    }

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.transform == player)
        {
            completedTimeSegment = true;
        }
    }
}//EndScript