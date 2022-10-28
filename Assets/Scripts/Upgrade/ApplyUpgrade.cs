using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyUpgrade : MonoBehaviour
{
    [HideInInspector] public string cellType;
    [SerializeField] private GameObject neutrofil;
    [SerializeField] private GameObject platelet;
    [SerializeField] private GameObject eosinophil;
    [SerializeField] private GameObject macrophage;

    public void UpgradeCell()
    {
        Debug.Log(cellType);
        switch (cellType)
        {
            case "Neutrophil":
                neutrofil.GetComponent<Neutrofil>().Upgrade();
                break;

            case "Platelet":
                platelet.GetComponent<Platelet>().Upgrade();
                break;

            case "Eosinophil":
                eosinophil.GetComponent<Eosinophil>().Upgrade();
                break;

            case "Macrophage":
                macrophage.GetComponent<Macrophage>().Upgrade();
                break;
        }
    }
}
