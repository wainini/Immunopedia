using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResizePopUpUI : MonoBehaviour
{
    [SerializeField] private GameObject skillNamePanel;
    [SerializeField] private GameObject skillDescriptionPanel;

    private SkillDescriptionPanelResize goDesc;
    private SkillNamePanelResize goName;

    private void Start()
    {
        goDesc = skillDescriptionPanel.GetComponent<SkillDescriptionPanelResize>();
        goName = skillNamePanel.GetComponent<SkillNamePanelResize>();
    }
    public void ResizeUI()
    {
        if (goDesc != null && goName != null)
        {
           goDesc.ResizePanel();
           goName.ResizePanel();
        }
    }
}
