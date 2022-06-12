using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingSpot : MonoBehaviour
{
    private BuildingManager buildingManager;
    void Start()
    {
        buildingManager = FindObjectOfType<BuildingManager>();
        GetComponent<Button>().onClick.AddListener(() => buildingManager.ViewPopUp(transform));
    }

}
