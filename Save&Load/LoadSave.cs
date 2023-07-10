using System; // convert.To lib
using System.IO; // File Lib
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSave : MonoBehaviour
{
    SaveGameData gameData;
    string saveFileName = @"C:\PixelAdventures\Save_01.txt", importSaveFileLocation = "/SavedGames/Save_01.txt";

    public void LoadGameSave()
    {
        if (File.Exists(saveFileName)) // if we have a save file on our system
        {
            BuildLoadFileInGame();
            StartCoroutine(BruteLoad());
        };
    }

    IEnumerator BruteLoad()
    {
        BuildLoadFileInGame();
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(BruteLoad());
    }

    void Start()
    {
        //gameData = (SaveGameData) GameObject.FindObjectOfType(typeof(SaveGameData));
        gameData = transform.GetComponent<SaveGameData>();
    }

    private IEnumerator LoadingData()
    {
        yield return new WaitForSeconds(0.35f);
        if (gameData != null)
        {
            TextReader tr = new StreamReader(Application.dataPath + importSaveFileLocation); // the TextReader reads the txt file and reads the lines to then be converted to game data

            // read lines of text
            string levelStr = tr.ReadLine();
            string currentObjectiveTextStr = tr.ReadLine();
            string playerPosStrX = tr.ReadLine();
            string playerPosStrY = tr.ReadLine();
            string playerPosStrZ = tr.ReadLine();
            string aiNum = tr.ReadLine();

            // convert string to datatype
            gameData.SetLevel(Convert.ToInt32(levelStr));
            // gameData.SetPlayerPos(new Vector3((float) Convert.ToDouble(playerPosStrX),(float) Convert.ToDouble(playerPosStrY),(float) Convert.ToDouble(playerPosStrZ)));

            PlayerPrefs.SetFloat("playerX", (float) Convert.ToDouble(playerPosStrX));
            PlayerPrefs.SetFloat("playerY", (float) Convert.ToDouble(playerPosStrY));
            PlayerPrefs.SetFloat("playerZ", (float) Convert.ToDouble(playerPosStrZ));
            PlayerPrefs.SetInt("AINum", Convert.ToInt32(aiNum));
            
            if (Convert.ToInt32(aiNum) > 0)
            {
                for (int i = 0; i < Convert.ToInt32(aiNum); i++)
                {
                    PlayerPrefs.SetString("AINum"+i, tr.ReadLine());
                }
            }

            int laserBoxLength = Convert.ToInt32(tr.ReadLine());
            if (laserBoxLength > 0)
            {
                for (int i = 0; i < laserBoxLength; i++)
                {
                    PlayerPrefs.SetString("laser"+i, tr.ReadLine());
                }
            }

            // close the stream     
            tr.Close();

            SceneManager.LoadScene(gameData.GetLevel());
        }
    }
    
    //=======================================================

    private IEnumerator CheckingForFile()
    {
        yield return new WaitForSeconds(0.5f);

        if (File.Exists(saveFileName))
        {
            StartCoroutine(CopyFile());
        }
    }

    private IEnumerator CopyFile()
    {
        yield return new WaitForSeconds(0.5f);
        BuildLoadFileInGame();
    }

    private void BuildLoadFileInGame()
    {
        // build new file
        if (!File.Exists(Application.dataPath + importSaveFileLocation))
        {
            print("Importing File. . .");
            File.Create(Application.dataPath + importSaveFileLocation);
            StartCoroutine(CheckingForFile());
        } else
            {
                // Application.dataPath Includes /Assets/
                File.Copy(saveFileName, Application.dataPath + importSaveFileLocation, true);
                print("Game Save Imported. . .");
                
                StartCoroutine(LoadingData());
            }
    }
}//EndScript