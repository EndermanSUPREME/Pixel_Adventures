using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelFourEndScript : MonoBehaviour
{
    [SerializeField] Transform player, warlock, endPos;
    [SerializeField] Sprite warlockLookRight;
    public float animationTimeLength, animatedWalkSpeed;
    bool beginEndScene = false;

    // Update is called once per frame
    void Update()
    {
        if (beginEndScene)
        {
            EndSceneAnimation();
        }
    }

    private IEnumerator AnimationLength()
    {
        yield return new WaitForSeconds(animationTimeLength);
    	SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void EndSceneAnimation()
    {
        if (player.position.x > endPos.position.x)
        {
            player.GetComponent<Animator>().Play("walkLeft");
            player.position = Vector2.MoveTowards(player.position, endPos.position, animatedWalkSpeed * Time.deltaTime);
        } else
            {
                player.GetComponent<Animator>().Play("IdleLeft");
            }

        if (player.position.x < endPos.position.x + 0.65f)
        {
            warlock.GetComponent<SpriteRenderer>().sprite = warlockLookRight;
        }
    }

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.transform == player)
        {
            beginEndScene = true;
            // game scripts are MonoBehaviours
            MonoBehaviour[] scripts = player.GetComponents<MonoBehaviour>();
            // creating an array of all scripts attached to an object allows us to surf a list of them to disable/enabled them all
            foreach(MonoBehaviour script in scripts)
            {
                script.enabled = false;
            }

            StartCoroutine(AnimationLength());
        }
    }
}//EndScript