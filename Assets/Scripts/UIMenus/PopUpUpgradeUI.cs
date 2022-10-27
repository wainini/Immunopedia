using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class PopUpUpgradeUI : MonoBehaviour, IPointerDownHandler
{
    public GameObject popUpPrefab;
    public RectTransform position;

    private GameObject popUp = null;

    public void OnPointerDown(PointerEventData eventData)
    {
        if(eventData.pointerPress == null)
        {
            Destroy(popUp);
            popUp = null;
        }
        Debug.Log(eventData.pointerPress);
    }

    public void PopUp(UpgradeData data)
    {
        if(popUp == null)
        {
            popUp = Instantiate(popUpPrefab, position);
        }
        TextMeshProUGUI[] texts = popUp.GetComponentsInChildren<TextMeshProUGUI>();

        texts[0].text = data.upgradeTitle;
        texts[1].text = data.upgradeDescription;
    }
}
