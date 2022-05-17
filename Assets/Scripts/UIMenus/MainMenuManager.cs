using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private GameObject settingsMenu;

    private void Awake()
    {
        SetVolume("MasterVol");
        SetVolume("BGMVol");
        SetVolume("SFXVol");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && settingsMenu.activeInHierarchy)
        {
            settingsMenu.SetActive(false);
        }
    }

    private void SetVolume(string group)
    {
        mixer.SetFloat(group, Mathf.Log10(PlayerPrefs.GetFloat(group, 1) * 20));
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
        settingsMenu.SetActive(true);
    }
}
