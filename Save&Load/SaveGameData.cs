using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveGameData : MonoBehaviour
{
    LevelInfo LevelData;
    public int level;
    Transform[] Enemies, ObjectiveTriggers, LevelEventTriggers, EventBorders, laserBoxStatus, elevatorPos;
    Vector3[] AI_Pos;
    Vector3 playerPos;
    string currentObjectiveText, saveFileName = @"C:\PixelAdventures\Save_01.txt", importSaveFileLocation = "/SavedGames/Save_01.txt";
    [SerializeField] Text objectiveDisplay;
    int timesPressed = 0;
    TextWriter tw;

    void Start()
    {
        LevelData = GetComponent<LevelInfo>();
        StartCoroutine(FetchData());
    }

    private IEnumerator FetchData()
    {
        yield return new WaitForSeconds(0.5f);

        if (LevelData != null)
        {
            level = LevelData.level;
            Enemies = LevelData.Enemies;
            ObjectiveTriggers = LevelData.ObjectiveTriggers;
            LevelEventTriggers = LevelData.LevelEventTriggers;
            EventBorders = LevelData.EventBorders;
        }
    }

    public void SaveGame()
    {
        timesPressed++;

        // build new directory on the players computer
        Directory.CreateDirectory(@"C:\PixelAdventures\");
        Directory.CreateDirectory(Application.dataPath + "/SavedGames/");

        SetFileData();
    }

    private void SetFileData()
    {
        //================= SAVE AI POS ====================
        int AI_Pos_Length = 0;

        foreach (Transform Enemy in Enemies)
        {
            if (Enemy != null)
            {
                AI_Pos_Length++;
            }
        }

        AI_Pos = new Vector3[AI_Pos_Length];

        int PosIndex = 0;
        foreach (Transform Enemy in Enemies)
        {
            if (Enemy != null)
            {
                AI_Pos[PosIndex] = Enemy.position;
                PosIndex++;
            }
        }
        //================= End OF SAVE AI POS ===============

        playerPos = LevelData.PlayerModel.position;
        
        if (objectiveDisplay != null)
        {
            currentObjectiveText = objectiveDisplay.text;
        }

        BuildSaveFile();
    }

    private void BuildSaveFile()
    {
        if (!File.Exists(saveFileName))
        {
            print("Creating File. . .");
            tw = new StreamWriter(saveFileName);
            StartCoroutine(CheckingForFile());
        } else // overwrite save
            {
                if (timesPressed > 1)
                {
                    tw = new StreamWriter(saveFileName);
                    WriteDataInFile();
                }
            }
    }

    private IEnumerator CheckingForFile()
    {
        yield return new WaitForSeconds(0.5f);

        if (File.Exists(saveFileName) && LevelData != null)
        {
            WriteDataInFile();
        } else
            {
                StartCoroutine(CheckingForFile());
            }
    }

    private void WriteDataInFile() // method to write to file
    {
        //======================================================================// The tw takes allows for data transfer, when we load the game data we convert strings to ints, doubles, and other types needed

        // write lines of text to the TextWriter
        tw.WriteLine(level);
        tw.WriteLine(currentObjectiveText);

        if (level < 5)
        {
            tw.WriteLine(playerPos.x);
            tw.WriteLine(playerPos.y);
            tw.WriteLine(playerPos.z);
        }

        if (Enemies != null)
        {
            int i2 = 0;
            for (int i = 0; i < Enemies.Length; i++)
            {
                if (Enemies[i] != null)
                {
                    i2++;
                }
            }

            tw.WriteLine(i2); // number of AI entries
            string[] currentEnemies = new string[i2];
            int i3 = 0;
            for (int i = 0; i < Enemies.Length; i++)
            {
                if (Enemies[i] != null)
                {
                    currentEnemies[i3] = Enemies[i].name;
                    tw.WriteLine(currentEnemies[i3]);
                    i3++;
                }
            }
        }
        
        if (laserBoxStatus != null)
        {
            tw.WriteLine(laserBoxStatus.Length);
            for (int i = 0; i < laserBoxStatus.Length; i++)
            {
                tw.WriteLine(laserBoxStatus[i].GetComponent<laserBox>().poweredOn);
            }
        }

        // close the stream
        tw.Close();
        //======================================================================//

        print("File Created and Saved. . .");
    }

    //===================================================//

    public int GetLevel()
    {
        return level;
    }

    public void SetLevel(int importedLevel)
    {
        level = importedLevel;
    }

    public Transform[] GetAI()
    {
        return Enemies;
    }

    public Transform[] GetObjectiveTriggers()
    {
        return ObjectiveTriggers;
    }

    public Transform[] GetLevelEventTriggers()
    {
        return LevelEventTriggers;
    }

    public Transform[] GetEventBorders()
    {
        return EventBorders;
    }

    public Vector3[] Get_AI_Pos()
    {
        return AI_Pos;
    }
}//EndScript