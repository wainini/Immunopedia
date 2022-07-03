using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BoneMarrow : MonoBehaviour
{
    private BuildingManager buildingManager;
    private void Start()
    {
        AudioManager.instance.PlaySound("BuildBoneMarrow", SoundOutput.sfx);
        buildingManager = BuildingManager.instance;
        GetComponent<Button>().onClick.AddListener(() => buildingManager.OpenPopUp(transform));
    }

    private void FixedUpdate()
    {
        buildingManager.resource += Time.fixedDeltaTime;
    }

    public void OnBoneMarrowClick()
    {
        AudioManager.instance.PlaySound("ClickBuilding", SoundOutput.sfx);
    }

}