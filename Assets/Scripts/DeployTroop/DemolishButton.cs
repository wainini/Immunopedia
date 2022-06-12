using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DemolishButton : MonoBehaviour
{

    private BuildingManager buildingManager;
    private void Start()
    {
        buildingManager = FindObjectOfType<BuildingManager>();
        GetComponent<Button>().onClick.AddListener(() => buildingManager.DemolishBuilding(transform.parent.parent.parent));
    }


}
