using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DemolishButton : MonoBehaviour
{

    private BuildingManager buildingManager;
    private void Start()
    {
        buildingManager = BuildingManager.instance;
        GetComponent<Button>().onClick.AddListener(() => buildingManager.DemolishBuilding());
    }


}
