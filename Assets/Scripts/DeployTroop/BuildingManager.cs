using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        GameObject building = Instantiate(buildingPrefab, Vector2.zero, Quaternion.identity, this.transform);
        building.GetComponentInChildren<Button>().transform.position = selectedSpot.position;
        Destroy(selectedSpot.gameObject);
        ExitPopUp();
    }
}
