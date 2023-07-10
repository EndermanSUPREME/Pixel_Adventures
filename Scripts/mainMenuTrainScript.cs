using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainMenuTrainScript : MonoBehaviour
{
    int Timer;
    [SerializeField] Transform Train, Spawn;
    [Range(20, 40)] public float speed;

    // Start is called before the first frame update
    void Start()
    {
        Timer = Random.Range(2, 4);
        StartCoroutine(SpawnTrain(Timer));
    }

    void FixedUpdate()
    {
        Train.position = Vector2.MoveTowards(Train.position, new Vector2(45, Train.position.y), speed * Time.deltaTime);
    }

    private IEnumerator SpawnTrain(float time)
    {
        yield return new WaitForSeconds(time);
        Train.position = Spawn.position;

        Timer = Random.Range(2, 5);
        StartCoroutine(SpawnTrain(Timer));
    }
}//EndScript