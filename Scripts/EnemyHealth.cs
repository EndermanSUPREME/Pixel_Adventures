using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int TotalHealth;
    [SerializeField] private GameObject AI;

    public AudioSource damaged, death;

    public void TakeDamage(int amount)
    {
        damaged.Play();

        TotalHealth -= amount;

        if (TotalHealth <= 0)
        {
            death.Play();
            Destroy(AI);
        }
    }
}//EndScript