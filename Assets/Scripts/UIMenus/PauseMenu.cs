using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject settingsMenu;

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
        gameObject.SetActive(false);
        PauseMenuManager.menuState = PauseMenuManager.MenuState.inGame;
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        PauseMenuManager.menuState = PauseMenuManager.MenuState.inGame;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
        PauseMenuManager.menuState = PauseMenuManager.MenuState.inGame;
    }
    public void Settings()
    {
        settingsMenu.SetActive(true);
        PauseMenuManager.menuState = PauseMenuManager.MenuState.inSettingsMenu;
    }
}
