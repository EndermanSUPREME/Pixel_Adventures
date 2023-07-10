using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ComputerLevelTransition : MonoBehaviour
{
    public GameObject Pop_up;
    public Transform player;

    void Start()
    {
        Pop_up.SetActive(false);
    }

    void Update()
    {
        bool E = Input.GetKeyDown(KeyCode. E);

        if (Pop_up.active && E)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.transform == player)
        {
            Pop_up.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D collider2D)
    {
        if (collider2D.transform == player)
        {
            Pop_up.SetActive(false);
        }
    }
}//EndScript