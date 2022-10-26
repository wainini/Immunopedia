using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PopUpUpgradeUI : MonoBehaviour
{
    public GameObject popUpPrefab;
    
    public void PopUp(UpgradeData data)
    {
        GameObject popUp = Instantiate(popUpPrefab);
        TextMeshProUGUI[] texts = popUp.GetComponentsInChildren<TextMeshProUGUI>();

        texts[0].text = data.upgradeTitle;
        texts[1].text = data.upgradeDescription;
    }
}
