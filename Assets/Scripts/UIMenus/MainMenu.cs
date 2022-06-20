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

    private void Awake()
    {
        menuManager = MenuManager.instance;
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
}
