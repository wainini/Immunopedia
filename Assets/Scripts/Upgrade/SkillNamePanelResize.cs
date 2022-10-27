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
        RectTransform textRectTransform = skillName.GetComponent<RectTransform>();
        TextMeshProUGUI tmp = skillName.GetComponent<TextMeshProUGUI>();
        tmp.ForceMeshUpdate();

        Vector3 newPanelSize = tmp.textBounds.size;
        newPanelSize.x += Mathf.Abs(textRectTransform.localPosition.x * 2);
        //print(newPanelSize.y);
        newPanelSize.y += 20f;
        panelTransform.sizeDelta = newPanelSize;

    }
}
