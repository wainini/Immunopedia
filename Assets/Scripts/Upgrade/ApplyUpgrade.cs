using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyUpgrade : MonoBehaviour
{
    [HideInInspector] public string cellType;
    [HideInInspector] public int upCost;
    [SerializeField] private GameObject neutrofil;
    [SerializeField] private GameObject platelet;
    [SerializeField] private GameObject eosinophil;
    [SerializeField] private GameObject macrophage;

    public void UpgradeCell()
    {
        if(PlayerPrefs.GetInt("Total Stars") < upCost)
        {
            return;
        }
        //Debug.Log(cellType);
        switch (cellType)
        {
            case "Neutrophil":
                neutrofil.GetComponentInChildren<Neutrofil>().Upgrade();
                break;

            case "Platelet":
                platelet.GetComponent<Platelet>().Upgrade();
                break;

            case "Eosinophil":
                eosinophil.GetComponentInChildren<Eosinophil>().Upgrade();
                break;

            case "Macrophage":
                macrophage.GetComponentInChildren<Macrophage>().Upgrade();
                break;
        }
        int totalStars = PlayerPrefs.GetInt("Total Stars");
        totalStars -= upCost;

        int usedStars = PlayerPrefs.GetInt("Stars Used");
        usedStars += upCost;

        PlayerPrefs.SetInt("Total Stars", totalStars);
        PlayerPrefs.SetInt("Stars Used", usedStars);

        UpdateUI();
        Destroy(gameObject);
    }

    private void UpdateUI()
    {
        GameObject canvas = gameObject.transform.parent.gameObject;
        canvas.GetComponent<UpdateUI>().RefreshUI();
    }
}
