using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PowerUpUI : MonoBehaviour
{
    public TextMeshProUGUI totalStars;
    public int sceneCount;

    public GameObject neutro;
    public GameObject eosi;
    public GameObject platelet;
    public GameObject macro;

    public Button[] neutroUpBtn;
    public Button[] plateletUpBtn;
    public Button[] eosiUpBtn;
    public Button[] macroUpBtn;

    void Start()
    {
        totalStars.text = PlayerPrefs.GetInt("Total Stars").ToString();
        if(!PlayerPrefs.HasKey("Stars Used"))
        {
            PlayerPrefs.SetInt("Stars Used", 0);
        }


        string nKey = "NeutrofilUpLvl";
        string pKey = "PlateletUpLvl";
        string eKey = "EosinophilUpLvl";
        string mKey = "MacrophageUpLvl";
        //Enable button Neutrofil
        int idx = PlayerPrefs.GetInt(nKey);
        for (int i = 0; i < idx + 1; i++)
        {
            neutroUpBtn[i].interactable = true;
        }

        //Enable button Platelet
        idx = PlayerPrefs.GetInt(pKey);
        for (int i = 0; i < idx + 1; i++)
        {
            plateletUpBtn[i].interactable = true;
        }

        //Enable button Eosinophil
        idx = PlayerPrefs.GetInt(eKey);
        for (int i = 0; i < idx + 1; i++)
        {
            eosiUpBtn[i].interactable = true;
        }

        //Enable button Macrophage
        idx = PlayerPrefs.GetInt(mKey);
        for (int i = 0; i < idx + 1; i++)
        {
            macroUpBtn[i].interactable = true;
        }
    }

    public void ResetUpgrades()
    {
        //Debug.Log("Reset all upgrades");
        ResetNeutrophil();
        ResetEosinophil();
        ResetPlatelet();
        ResetMacrophage();
        int revertStars = PlayerPrefs.GetInt("Total Stars") + PlayerPrefs.GetInt("Stars Used");
        PlayerPrefs.SetInt("Stars Used", 0);
        PlayerPrefs.SetInt("Total Stars", revertStars);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void ResetNeutrophil()
    {
        Neutrofil n = neutro.GetComponent<Neutrofil>();
        n.stats.ResetStats();
        if (PlayerPrefs.GetInt(n.key) >= 4)
        {
            n.trainData.cost++;
        }
        PlayerPrefs.SetInt(n.key, 0);
    }

    void ResetEosinophil()
    {
        Eosinophil e = eosi.GetComponent<Eosinophil>();
        e.stats.ResetStats();
        if (PlayerPrefs.GetInt(e.key) >= 4)
        {
            e.movSpeedReduction = 0.33f;
        }
        PlayerPrefs.SetInt(e.key, 0);
    }

    void ResetPlatelet()
    {
        Platelet p = platelet.GetComponent<Platelet>();
        int costReduction = (PlayerPrefs.GetInt(p.key) >= 4) ? 3 : PlayerPrefs.GetInt(p.key);
        p.trainData.cost -= costReduction;
        if (PlayerPrefs.GetInt(p.key) >= 4)
        {
            p.Revert();
        }
        PlayerPrefs.SetInt(p.key, 0);
    }

    void ResetMacrophage()
    {
        Macrophage m = macro.GetComponent<Macrophage>();
        m.stats.ResetStats();
        if (PlayerPrefs.GetInt(m.key) >= 4)
        {
            m.stats.blockCount--;
        }
        PlayerPrefs.SetInt(m.key, 0);
    }

    public void GoToLevelSelector()
    {
        SceneManager.LoadScene(0);
    }
}
