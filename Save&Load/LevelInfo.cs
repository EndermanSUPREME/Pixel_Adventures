using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelInfo : MonoBehaviour
{
    public Transform[] Enemies, ObjectiveTriggers, LevelEventTriggers, EventBorders, LaserBoxes, Elevators;
    public Transform PlayerModel;
    public int level;

    private float resBar, musicBar, sfxBar;
    private MainMenuUI settings;

    void Start()
    {
        settings = (MainMenuUI) GameObject.FindObjectOfType(typeof(MainMenuUI));

        GameObject player = GameObject.Find("PlayerModel");
        PlayerModel = player.transform;
        level = SceneManager.GetActiveScene().buildIndex;

        Screen.SetResolution(PlayerPrefs.GetInt("Width"), PlayerPrefs.GetInt("Height"), true);
        musicBar = PlayerPrefs.GetFloat("MusicSlideVal");
        sfxBar = PlayerPrefs.GetFloat("SFXSlideVal");
        resBar = PlayerPrefs.GetFloat("ResSlideVal");

        GettingPlayerPos();
        StartCoroutine(DeletePrefs());
    }

    private IEnumerator DeletePrefs()
    {
        yield return new WaitForSeconds(0.15f);
        PlayerPrefs.DeleteAll();

        StartCoroutine(ResetSettings());
    }

    private IEnumerator ResetSettings()
    {
        PlayerPrefs.SetInt("Width", Screen.width);
        PlayerPrefs.SetInt("Height", Screen.height);
        PlayerPrefs.SetFloat("MusicSlideVal", musicBar);
        PlayerPrefs.SetFloat("SFXSlideVal", sfxBar);
        PlayerPrefs.SetFloat("ResSlideVal", resBar);

        print(PlayerPrefs.GetFloat("MusicSlideVal") + " " + PlayerPrefs.GetFloat("SFXSlideVal") + " " + PlayerPrefs.GetFloat("ResSlideVal"));

        yield return new WaitForSeconds(0.25f);
        settings.ResetMySettings();
    }

    private void GettingPlayerPos()
    {
        // print("Getting Player Prefs");

        float x,y,z;
        x = PlayerPrefs.GetFloat("playerX");
        y = PlayerPrefs.GetFloat("playerY");
        z = PlayerPrefs.GetFloat("playerZ");

        if (new Vector3(x,y,z) != new Vector3(0,0,0))
        {
            // print("PlayerPos is : " + new Vector3(x,y,z));
            SetPlayerPos(x, y, z);
            GetLevelAI();
            GetLevelLasers();
        } else
            {
                // print("No Prefs");
            }
    }

    private void SetPlayerPos(float x, float y, float z)
    {
        PlayerModel.position = new Vector3(x,y,z);
    }

    private void GetLevelAI()
    {
        int num = PlayerPrefs.GetInt("AINum");
        string[] aiNames = new string[num];

            for (int i = 0; i < aiNames.Length; i++)
            {
                aiNames[i] = PlayerPrefs.GetString("AINum"+i);
            }

            for (int i = 0; i < Enemies.Length; i++)
            {
                if (!aiNames.Contains(Enemies[i].name))
                {
                    Destroy(Enemies[i].gameObject);
                }
            }
    }

    private void GetLevelLasers()
    {
        int num = PlayerPrefs.GetInt("LaserNum");

            for (int i = 0; i < LaserBoxes.Length; i++)
            {
                if (PlayerPrefs.GetString("laser"+i) == "true")
                {
                    LaserBoxes[i].GetComponent<laserBox>().poweredOn = true;
                } else
                    {
                        LaserBoxes[i].GetComponent<laserBox>().poweredOn = false;
                    }
            }
    }
}//EndScript