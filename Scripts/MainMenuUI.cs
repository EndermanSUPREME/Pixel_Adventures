using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    private float musicVolume, sfxVolume, ResolutionIndex;
    int PauseInt = 0;
    private int[] ScreenWidth = {800, 1024, 1152, 1280, 1280, 1440, 1600, 1920, 2560}, ScreenHeight = {600, 768, 864, 720, 1024, 900, 1200, 1080, 1440}; // [0, 8]
    [SerializeField] Slider MusicSlider, SfxSlider, ResolutionSlider;
    [SerializeField] GameObject[] MusicComponent, SFXComponent;
    [SerializeField] GameObject Main, Setting;
    [SerializeField] Text MusicDisplay, SFX_Display, ResolutionDisplay;

    void Start()
    {
        Application.targetFrameRate = 60;

        // Get all objects in the scene that have a script for editing
        MusicComponent = GameObject.FindGameObjectsWithTag("Music");
        SFXComponent = GameObject.FindGameObjectsWithTag("SFX");

        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            GoToMain();

            ResolutionSlider.value = 7;
            ResolutionIndex = ResolutionSlider.value;

            MusicSlider.value = 3;
            SfxSlider.value = 3;
            
            SaveAndApply();
        } else
            {
                Screen.SetResolution(PlayerPrefs.GetInt("Width"), PlayerPrefs.GetInt("Height"), true);

                Main.SetActive(false);
                Setting.SetActive(false);
                GetGameSettings();
            }
    }

    public void ResetMySettings()
    {
        MusicSlider.value = PlayerPrefs.GetFloat("MusicSlideVal");
        SfxSlider.value = PlayerPrefs.GetFloat("SFXSlideVal");

        ResolutionSlider.value = PlayerPrefs.GetFloat("ResSlideVal");

        SaveAndApply();

        print("Settings Resetted In New Level");
        print(PlayerPrefs.GetInt("Width") + " " + PlayerPrefs.GetInt("Height") + " = Sliders => " + PlayerPrefs.GetFloat("ResSlideVal") + " : " + PlayerPrefs.GetFloat("MusicVol") + " " + PlayerPrefs.GetFloat("SFXVol"));
    }

    private void GetGameSettings()
    {
        Screen.SetResolution(PlayerPrefs.GetInt("Width"), PlayerPrefs.GetInt("Height"), true);

        MusicSlider.value = PlayerPrefs.GetFloat("MusicVol") * 10;
        SfxSlider.value = PlayerPrefs.GetFloat("SFXVol") * 10;
        ResolutionSlider.value = PlayerPrefs.GetFloat("ResSlideVal");

        // print(PlayerPrefs.GetInt("Width") + " " + PlayerPrefs.GetInt("Height") + " " + PlayerPrefs.GetFloat("ResSlideVal") + " : " + PlayerPrefs.GetFloat("MusicVol") + " " + PlayerPrefs.GetFloat("SFXVol"));

        SaveAndApply();

        print("Settings Applied In New Level");
        print(PlayerPrefs.GetInt("Width") + " " + PlayerPrefs.GetInt("Height") + " = Sliders => " + PlayerPrefs.GetFloat("ResSlideVal") + " : " + PlayerPrefs.GetFloat("MusicVol") + " " + PlayerPrefs.GetFloat("SFXVol"));
    }

    public void SaveAndApply()
    {
        ResolutionSettings();
        AudioSettings();

        // print(PlayerPrefs.GetInt("Width") + " " + PlayerPrefs.GetInt("Height") + " " + PlayerPrefs.GetFloat("MusicVol") + " " + PlayerPrefs.GetFloat("SFXVol") + " " + PlayerPrefs.GetFloat("ResSlideVal"));
    }

    private void ResolutionSettings()
    {
        ResolutionIndex = ResolutionSlider.value;
        Screen.SetResolution(ScreenWidth[(int)ResolutionIndex], ScreenHeight[(int)ResolutionIndex], true);

        PlayerPrefs.SetInt("Width", ScreenWidth[(int)ResolutionIndex]);
        PlayerPrefs.SetInt("Height", ScreenHeight[(int)ResolutionIndex]);
        PlayerPrefs.SetFloat("ResSlideVal", ResolutionSlider.value);
    }

    private void AudioSettings()
    {
        musicVolume = MusicSlider.value / 10;
        sfxVolume = SfxSlider.value / 10;

        PlayerPrefs.SetFloat("MusicVol", musicVolume);
        PlayerPrefs.SetFloat("SFXVol", sfxVolume);

        PlayerPrefs.SetFloat("MusicSlideVal", MusicSlider.value);
        PlayerPrefs.SetFloat("SFXSlideVal", SfxSlider.value);

        foreach (GameObject Sound in MusicComponent)
        {
            Sound.GetComponent<AudioSource>().volume = musicVolume;
        }

        foreach (GameObject Sound in SFXComponent)
        {
            Sound.GetComponent<AudioSource>().volume = musicVolume;
        }
    }

    void Update()
    {
        if (Setting.active)
        {
            ResolutionIndex = ResolutionSlider.value;
            MusicDisplay.text = MusicSlider.value * 10 + " %";
            SFX_Display.text = SfxSlider.value * 10 + " %";
            ResolutionDisplay.text = ScreenWidth[(int)ResolutionIndex] + "x" + ScreenHeight[(int)ResolutionIndex];
        }

        if (SceneManager.GetActiveScene().buildIndex > 0 && SceneManager.GetActiveScene().buildIndex < 7) // 1-6
        {
            bool Escape = Input.GetKeyDown(KeyCode. Escape);

            if (Escape)
            {
                PauseInt++;

                switch (PauseInt)
                {
                    case 1:
                        GoToMain();
                    break;
                    case 2:
                        ResumeGame();
                        PauseInt = 0;
                    break;
                    default:
                    break;
                }
            }
        }
    }
//======================= SCREEN CHANGES ==========================
    public void GoToMain()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;

        Main.SetActive(true);
        Setting.SetActive(false);
    }

    public void GoToSettings()
    {
        Main.SetActive(false);
        Setting.SetActive(true);
    }

    public void StartTheGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ReturnToMainMenuScene()
    {
        SceneManager.LoadScene(0);
    }

    public void CloseTheGame()
    {
        Application.Quit();
    }

    public void ResumeGame()
    {
        PauseInt = 0;
        Main.SetActive(false);
        Setting.SetActive(false);
        SaveAndApply();

        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }
}//EndScript