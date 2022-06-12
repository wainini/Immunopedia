using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    [SerializeField] private GameObject selectBuildingPopUp;

    [Header("Prefabs")]
    [SerializeField] private GameObject buildingSpotPrefab;
    [SerializeField] private GameObject boneMarrowPrefab;

    private Transform selectedSpot;
    
    public void ViewPopUp(Transform buildingSpot)
    {
        selectBuildingPopUp.SetActive(true);
        selectedSpot = buildingSpot;
    }

    public void ExitPopUp()
    {
        selectBuildingPopUp.SetActive(false);
    }

    public void DemolishBuilding(Transform buildPos)
    {
        Instantiate(buildingSpotPrefab, buildPos.position, Quaternion.identity, this.transform);
        Destroy(buildPos.gameObject);
    }

    public void ConstructBuilding(GameObject buildingPrefab)
    {
        Instantiate(buildingPrefab, selectedSpot.position, Quaternion.identity, this.transform);
        Destroy(selectedSpot.gameObject);
        ExitPopUp();
    }
}
