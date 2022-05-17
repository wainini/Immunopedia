using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PauseMenuManager : MonoBehaviour
{
    public enum MenuState
    {
        inGame, inPauseMenu, inSettingsMenu, inGameOverMenu
    };

    public static MenuState menuState;

    [SerializeField] private GameObject pauseMenu, settingsMenu, gameOverMenu;

    private void Awake()
    {
        menuState = MenuState.inGame;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            switch (menuState)
            {
                case MenuState.inGame:
                    pauseMenu.SetActive(true);
                    menuState = MenuState.inPauseMenu;
                    break;
                case MenuState.inPauseMenu:
                    pauseMenu.SetActive(false);
                    menuState = MenuState.inGame;
                    break;
                case MenuState.inSettingsMenu:
                    settingsMenu.SetActive(false);
                    menuState = MenuState.inPauseMenu;
                    break;
            }
        }
    }
}
