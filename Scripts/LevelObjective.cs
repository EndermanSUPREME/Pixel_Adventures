using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelObjective : MonoBehaviour
{
    public string objectiveString;
    GameObject PlayerModel;
    Transform player;
    [SerializeField] Text objectivesText;

    // Start is called before the first frame update
    void Start()
    {
        PlayerModel = GameObject.Find("PlayerModel");
        player = PlayerModel.transform;
    }

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.transform == player)
        {
            objectivesText.text = objectiveString;
            Destroy(transform.gameObject);
        }
    }
}//EndScript