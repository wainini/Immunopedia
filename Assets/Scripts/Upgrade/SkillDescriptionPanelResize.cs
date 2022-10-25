using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SkillDescriptionPanelResize : MonoBehaviour
{
    [SerializeField] private GameObject skillDescription;
    private RectTransform panelTransform;
    
    void Start()
    {
        panelTransform = gameObject.GetComponent<RectTransform>();
        TextMeshProUGUI tmp = skillDescription.GetComponent<TextMeshProUGUI>();
        tmp.ForceMeshUpdate();

        Vector3 newPanelSize = tmp.textBounds.size;
        newPanelSize.x += skillDescription.GetComponent<RectTransform>().localPosition.x * 2;
        newPanelSize.y += skillDescription.GetComponent<RectTransform>().localPosition.y * 2;
        panelTransform.sizeDelta = newPanelSize;
    }
}
