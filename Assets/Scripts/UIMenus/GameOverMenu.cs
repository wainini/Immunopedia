using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
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
}
