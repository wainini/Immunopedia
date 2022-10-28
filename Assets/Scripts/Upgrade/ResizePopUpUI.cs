using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResizePopUpUI : MonoBehaviour
{
    [SerializeField] private GameObject skillNamePanel;
    [SerializeField] private GameObject skillDescriptionPanel;
    public void ResizeUI()
    {
        skillDescriptionPanel.GetComponent<SkillDescriptionPanelResize>().ResizePanel();
        skillNamePanel.GetComponent<SkillNamePanelResize>().ResizePanel();
    }
}
