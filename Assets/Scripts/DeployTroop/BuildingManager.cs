using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuildingManager : MonoBehaviour
{
    public static BuildingManager instance { get; private set; }
    [SerializeField] public List<CellTrainingData> cellDatas;
    [SerializeField] public float resource = 100f;
    [SerializeField] private TextMeshProUGUI resourceText;
    [Header("PopUps")]
    [SerializeField] private GameObject buildingSpotPopUp;
    [SerializeField] private GameObject boneMarrowPopUp;

    [Header("Prefabs")]
    [SerializeField] private GameObject buildingSpotPrefab;
    [SerializeField] private GameObject boneMarrowPrefab;


    private TextMeshProUGUI popUpResourceText;
    private Transform currentBuilding;
    private MenuManager menuManager;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
        menuManager = MenuManager.instance;
    }

    private void Update()
    {
        //if(popUpResourceText == null)
        //{
        //    popUpResourceText = boneMarrowPopUp.transform.Find("Resource").GetComponent<TextMeshProUGUI>();
        //}
        //popUpResourceText.text = Mathf.FloorToInt(resource).ToString();
        resourceText.text = Mathf.FloorToInt(resource).ToString();
    }

    public void OpenPopUp(Transform currBuilding)
    {
        if(currBuilding.GetComponentInChildren<BuildingSpot>() != null)
        {
            menuManager.OpenMenu(buildingSpotPopUp);
        }
        else if(currBuilding.GetComponent<BoneMarrow>() != null )
        {
            menuManager.OpenMenu(boneMarrowPopUp);
        }
        currentBuilding = currBuilding;
    }

    public void ExitPopUp()
    {
        menuManager.CloseMenu();
    }

    public void DemolishBuilding()
    {
        Instantiate(buildingSpotPrefab, currentBuilding.position, Quaternion.identity, this.transform);
        Destroy(currentBuilding.gameObject);
        ExitPopUp();
    }

    public void ConstructBuilding(GameObject buildingPrefab)
    {
        GameObject building = Instantiate(buildingPrefab, Vector2.zero, Quaternion.identity, this.transform);
        building.GetComponentInChildren<Button>().transform.position = currentBuilding.position;
        Destroy(currentBuilding.gameObject);
        ExitPopUp();
    }
}
