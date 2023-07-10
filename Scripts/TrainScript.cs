using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainScript : MonoBehaviour
{
    public Transform player, Train, SubwayLights, stopLocation;
    [SerializeField] float speed, illusionSpeed, brakeRate;
    public GameObject[] TrainBrakes, Enemies;
    public bool TrainSequence = false;
    public bool fightSequence = false;
    [SerializeField] int enemyNum = 0;

    // Start is called before the first frame update
    void Start()
    {
        player.parent = null;

        for (int i = 0; i < TrainBrakes.Length; i++)
        {
            TrainBrakes[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (TrainSequence)
        {
            if (Train.position.x < 47.8374f)
            {
                Train.position = new Vector3(Train.position.x + speed, Train.position.y, Train.position.z);
            } else
                {
                    Train.position = new Vector3(47.8374f, Train.position.y, Train.position.z);

                    if (SubwayLights.position.x > -24)
                    {
                        SubwayLights.position = new Vector3(SubwayLights.position.x - illusionSpeed, SubwayLights.position.y, SubwayLights.position.z);
                    } else
                        {
                            SubwayLights.position = new Vector3(0, SubwayLights.position.y, SubwayLights.position.z);
                        }

                    fightSequence = true;
                }
        }

        if (fightSequence == true)
        {
            if (Enemies[enemyNum] == null && Enemies[enemyNum + 1] == null && enemyNum >= 12)
            {
                for (int i = 0; i < TrainBrakes.Length; i++)
                {
                    TrainBrakes[i].SetActive(true);
                }

                TrainSequence = false;
                fightSequence = false;
            }

            // couple waves of enemies spawn
            if (Enemies[enemyNum] == null && Enemies[enemyNum + 1] == null && (enemyNum + 2) < Enemies.Length) // the ai are dead
            {
                enemyNum += 2;
            } else // ai are alive
                {
                    if (Enemies[enemyNum].active == false && Enemies[enemyNum + 1].active == false)
                    {
                        Enemies[enemyNum].SetActive(true);
                        Enemies[enemyNum + 1].SetActive(true);
                    }
                }
        }

        if (player.parent != null && fightSequence == false && TrainSequence == false && enemyNum >= 12)
        {
            TrainStop();
        }
    }

    private void TrainStop()
    {
            SubwayLights.position = new Vector3(0, SubwayLights.position.y, SubwayLights.position.z);
            
            Train.position = Vector2.MoveTowards(Train.position, stopLocation.position, brakeRate * Time.deltaTime);

            if (Vector2.Distance(Train.position, stopLocation.position) < 20 && brakeRate > 8)
            {
                brakeRate = brakeRate - 0.05f;
            }
    }

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.transform == player)
        {
            player.SetParent(Train);
            player.GetComponent<playerMovement>().hidden = false;
        }
    }

    public void StartTrain()
    {
        TrainSequence = true;
    }
}//EndScript