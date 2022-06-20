using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    private MenuManager menuManager;
    [SerializeField] private GameObject settingsMenu;

    private void Awake()
    {
        menuManager = MenuManager.instance;
    }

    private void OnEnable()
    {
        Time.timeScale = 0f;
    }
    private void OnDisable()
    {
        Time.timeScale = 1f;
    }

    public void Continue()
    {
        menuManager.CloseMenu();
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void Settings()
    {
        menuManager.OpenMenu(settingsMenu);
    }
}
