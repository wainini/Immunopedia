using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
        int total = 0;
        for(int i = 1; i <= sceneCount; i++)
        {
            total += PlayerPrefs.GetInt("Level " + i);
        }
        totalStars.text = total.ToString();
    }

    public void ResetUpgrades()
    {
        Debug.Log("Reset all upgrades");
        //ResetNeutrophil();
        //ResetEosinophil();
        //ResetPlatelet();
        //ResetMacrophage();
    }

    void ResetNeutrophil()
    {
        Neutrofil n = neutro.GetComponent<Neutrofil>();
        n.stats.ResetStats();
        if(PlayerPrefs.GetInt(n.key) >= 4)
        {
            n.trainData.cost++;
        }
    }

    void ResetEosinophil()
    {
        Eosinophil e = eosi.GetComponent<Eosinophil>();
        e.stats.ResetStats();
        if(PlayerPrefs.GetInt(e.key) >= 4)
        {
            e.movSpeedReduction = 0.33f;
        }
    }

    void ResetPlatelet()
    {
        Platelet p = platelet.GetComponent<Platelet>();
        int costReduction = (PlayerPrefs.GetInt(p.key) >= 4) ? 3 : PlayerPrefs.GetInt(p.key);
        p.trainData.cost -= costReduction;
        if(PlayerPrefs.GetInt(p.key) >= 4)
        {
            p.Revert();
        }
    }

    void ResetMacrophage()
    {
        Macrophage m = macro.GetComponent<Macrophage>();
        m.stats.ResetStats();
        if(PlayerPrefs.GetInt(m.key) >= 4)
        {
            m.stats.blockCount--;
        }
    }
}
