using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingSpot : MonoBehaviour
{
    private BuildingManager buildingManager;
    void Start()
    {
        buildingManager = BuildingManager.instance;
        GetComponent<Button>().onClick.AddListener(() => buildingManager.OpenPopUp(transform.parent.transform));
    }

}
