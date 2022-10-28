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

    string nKey, pKey, eKey, mKey;

    void Start()
    {
        nKey = "NeutrofilUpLvl";
        pKey = "PlateletUpLvl";
        eKey = "EosinophilUpLvl";
        mKey = "MacrophageUpLvl";

        UpdateStarsQty();
        EnableButtons();
    }

    public void UpdateStarsQty()
    {
        totalStars.text = PlayerPrefs.GetInt("Total Stars").ToString();
        if (!PlayerPrefs.HasKey("Stars Used"))
        {
            PlayerPrefs.SetInt("Stars Used", 0);
        }
    }

    public void EnableButtons()
    {
        //Enable button Neutrofil
        int idx = PlayerPrefs.GetInt(nKey);
        idx = (idx == 4) ? 3 : idx;
        for (int i = 0; i <= idx; i++)
        {
            neutroUpBtn[i].interactable = true;
            if(i < PlayerPrefs.GetInt(nKey))
            {
                neutroUpBtn[i].onClick = null;
            }
        }

        //Enable button Platelet
        idx = PlayerPrefs.GetInt(pKey);
        idx = (idx == 4) ? 3 : idx;
        for (int i = 0; i <= idx; i++)
        {
            plateletUpBtn[i].interactable = true;
            if (i < PlayerPrefs.GetInt(pKey))
            {
                plateletUpBtn[i].onClick = null;
            }
        }

        //Enable button Eosinophil
        if(PlayerPrefs.GetInt("Eosinophil", 0) == 1)
        {
            idx = PlayerPrefs.GetInt(eKey);
            idx = (idx == 4) ? 3 : idx;
            for (int i = 0; i <= idx; i++)
            {
                eosiUpBtn[i].interactable = true;
                if (i < PlayerPrefs.GetInt(eKey))
                {
                    eosiUpBtn[i].onClick = null;
                }
            }
        }

        //Enable button Macrophage
        if(PlayerPrefs.GetInt("Macrophage", 0) == 1)
        {
            idx = PlayerPrefs.GetInt(mKey);
            idx = (idx == 4) ? 3 : idx;
            for (int i = 0; i <= idx; i++)
            {
                macroUpBtn[i].interactable = true;
                if (i < PlayerPrefs.GetInt(mKey))
                {
                    macroUpBtn[i].onClick = null;
                }
            }
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
        Neutrofil n = neutro.GetComponentInChildren<Neutrofil>();
        Debug.Log(n);
        n.stats.ResetStats();
        if (PlayerPrefs.GetInt(n.key) >= 4)
        {
            n.trainData.cost++;
        }
        PlayerPrefs.SetInt(n.key, 0);
    }

    void ResetEosinophil()
    {
        Eosinophil e = eosi.GetComponentInChildren<Eosinophil>();
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
        p.trainData.cost += costReduction;
        if (PlayerPrefs.GetInt(p.key) >= 4)
        {
            p.Revert();
        }
        PlayerPrefs.SetInt(p.key, 0);
    }

    void ResetMacrophage()
    {
        Macrophage m = macro.GetComponentInChildren<Macrophage>();
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
