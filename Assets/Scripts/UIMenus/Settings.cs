using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    private MenuManager menuManager;
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private Slider master, bgm, sfx;
    private void Awake()
    {
        menuManager = MenuManager.instance;
        //master.value = PlayerPrefs.GetFloat("MasterVol", 1);
        bgm.value = PlayerPrefs.GetFloat("BGMVol", 1);
        sfx.value = PlayerPrefs.GetFloat("SFXVol", 1);
    }

    public void ExitSettings()
    {
        menuManager.CloseMenu();
    }

    public void SetMasterVol(float value)
    {
        float volume = Mathf.Log10(value) * 20;
        mixer.SetFloat("MasterVol", volume);
        PlayerPrefs.SetFloat("MasterVol", value);
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
