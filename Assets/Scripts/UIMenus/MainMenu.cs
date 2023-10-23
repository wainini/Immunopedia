using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MainMenu : MonoBehaviour
{
    private MenuManager menuManager;
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private GameObject levelSelector;

    private void Awake()
    {
        menuManager = MenuManager.instance;
    }

    private void OnEnable()
    {
        Time.timeScale = 1f;
    }
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Settings()
    {
        menuManager.OpenMenu(settingsMenu);
    }

    public void LevelSelector()
    {
        menuManager.OpenMenu(levelSelector);
    }

    public void Almanac()
    {
        menuManager.OpenAlmanac();
    }
}
