using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelFiveBeginningSegment : MonoBehaviour
{
    MonoBehaviour[] playerScripts;
    [SerializeField] GameObject Timer;
    GameObject Player;
    Animator PlayerAnim;
    [SerializeField] Transform StartLevelPos;
    [SerializeField] Collider2D border;

    private bool playerMoveIn = false;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;

        Timer.SetActive(false);

        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;

        Player = GameObject.Find("PlayerModel");
        PlayerAnim = Player.transform.GetComponent<Animator>();

        border.enabled = false;

        playerScripts = Player.transform.GetComponents<MonoBehaviour>();
        foreach (MonoBehaviour script in playerScripts)
        {
            script.enabled = false;
        }
    }

    void Update()
    {
        if (playerMoveIn)
        {
            if (Player.transform.position.x != StartLevelPos.position.x)
            {
                Player.transform.position = Vector2.MoveTowards(Player.transform.position, StartLevelPos.position, 2 * Time.deltaTime);
                PlayerAnim.Play("walkRight");
            } else
                {
                    border.enabled = true;

                    PlayerAnim.Play("IdleRight");

                    foreach (MonoBehaviour script in playerScripts)
                    {
                        script.enabled = true;
                    }

                    Timer.SetActive(true);

                    transform.GetComponent<LevelFiveBeginningSegment>().enabled = false;
                }
        }
    }

    public void SnapToPlayer() // animation event
    {
        Camera.main.transform.position = new Vector3(0, 0, -10);

        playerMoveIn = true;
    }
}//EndScript