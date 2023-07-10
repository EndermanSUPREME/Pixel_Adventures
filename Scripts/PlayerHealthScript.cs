using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealthScript : MonoBehaviour
{
    [SerializeField] private int TotalHealth;
    public GameObject HealthBarIcon;
    [SerializeField] private GameObject[] HealthBar;
    public Transform UI_Parent;
    public int offset;
    public AudioSource injury;

    void Start()
    {
        HealthBar = new GameObject[TotalHealth];

        for (int i = 0; i < HealthBar.Length; i++)
        {
            HealthBar[i] = Instantiate(HealthBarIcon, new Vector3(HealthBarIcon.transform.position.x + (offset * i), HealthBarIcon.transform.position.y, HealthBarIcon.transform.position.z), HealthBarIcon.transform.rotation);
            HealthBar[i].transform.SetParent(UI_Parent);
            HealthBar[i].SetActive(true);
        }
    }

    public void TakeDamage(int amount)
    {
        injury.Play();
        
        TotalHealth -= amount;

        for (int i = 0; i < HealthBar.Length; i++)
        {
            if (i >= TotalHealth)
            {
                HealthBar[i].SetActive(false);
            }
        }

        if (TotalHealth <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}//EndScript