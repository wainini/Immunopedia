using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SkillNamePanelResize : MonoBehaviour
{
    [SerializeField] private GameObject skillName;
    private RectTransform panelTransform;

    void Start()
    {
        panelTransform = gameObject.GetComponent<RectTransform>();
        TextMeshProUGUI tmp = skillName.GetComponent<TextMeshProUGUI>();
        tmp.ForceMeshUpdate();
        Vector3 newPanelSize = tmp.textBounds.size;
        newPanelSize.x += skillName.GetComponent<RectTransform>().localPosition.x * 2;
        newPanelSize.y = panelTransform.rect.height;
        panelTransform.sizeDelta = newPanelSize;

    }
}
