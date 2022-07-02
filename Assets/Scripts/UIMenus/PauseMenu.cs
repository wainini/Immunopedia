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
    [SerializeField] private GameObject almanacMenu;
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private Slider master, bgm, sfx;

    private AreYouSure dialogPopUp;
    private void Awake()
    {
        menuManager = MenuManager.instance;
        master.value = PlayerPrefs.GetFloat("MasterVol", 1);
        bgm.value = PlayerPrefs.GetFloat("BGMVol", 1);
        sfx.value = PlayerPrefs.GetFloat("SFXVol", 1);
    }
    public void Continue()
    {
        menuManager.CloseMenu();
    }

    public void RestartLevelDialog()
    {
        menuManager.OpenDialogPopUp();
        dialogPopUp = (dialogPopUp) ? dialogPopUp : AreYouSure.instance;
        dialogPopUp.OpenDialogPopUp("Are you sure you want to restart the level?", RestartLevel);
    }

    public void MainMenuDialog()
    {
        menuManager.OpenDialogPopUp();
        dialogPopUp = (dialogPopUp) ? dialogPopUp : AreYouSure.instance;
        dialogPopUp.OpenDialogPopUp("Are you sure you want to go back to main menu?", MainMenu);
    }

    private void RestartLevel(bool yes)
    {
        dialogPopUp.RemoveListener();
        if(yes) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        else menuManager.CloseMenu();
    }

    private void MainMenu(bool yes)
    {
        dialogPopUp.RemoveListener();
        if(yes) SceneManager.LoadScene(0);
        else menuManager.CloseMenu();
    }
    public void Settings()
    {
        menuManager.OpenMenu(settingsMenu);
    }

    public void Almanac()
    {
        menuManager.OpenMenu(almanacMenu);
    }

    public void SetBGMVol(float value)
    {

        float volume = Mathf.Log10(value) * 20;
        mixer.SetFloat("BGMVol", volume);
        PlayerPrefs.SetFloat("BGMVol", value);
    }
    public void SetSFXVol(float value)
    {
        float volume = Mathf.Log10(value) * 20;
        mixer.SetFloat("SFXVol", volume);
        PlayerPrefs.SetFloat("SFXVol", value);
    }
}
