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
        ResizePanel();
    }

    public void ResizePanel()
    {
        panelTransform = gameObject.GetComponent<RectTransform>();
        RectTransform skillDescTransform = skillDescription.GetComponent<RectTransform>();
        TextMeshProUGUI tmp = skillDescription.GetComponent<TextMeshProUGUI>();
        tmp.ForceMeshUpdate();

        Vector3 newPanelSize = tmp.textBounds.size;
        newPanelSize.x += Mathf.Abs(skillDescTransform.localPosition.x * 2);

        if (tmp.textInfo.lineCount == 1)
        {
            newPanelSize.y *= 2;
        }
        newPanelSize.y += Mathf.Abs(skillDescTransform.localPosition.y * 2);

        panelTransform.sizeDelta = newPanelSize;
    }
}
