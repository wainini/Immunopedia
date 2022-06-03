using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CellsSelectDeploy : MonoBehaviour
{
    public class AvailableCellsData
    {
        public CellTrainingData cellData;
        public GameObject selectableCellUI;
        public int amount;

        public AvailableCellsData(CellTrainingData cellData, GameObject selectableCellUI, int amount)
        {
            this.cellData = cellData;
            this.selectableCellUI = selectableCellUI;
            this.amount = amount;
        }
    }
    private List<AvailableCellsData> availableCells = new List<AvailableCellsData>();

    [SerializeField] private GameObject selectableUIPrefab;
    private ToggleGroup layoutToggleGroup;
    private GameObject selectedCellPrefab;


    private Camera mainCam;
    private Vector3 mousePos;

    [Header("Deploy")]
    [SerializeField] private LayerMask deployLayer;
    [SerializeField] private float deployCooldown = 0.5f;
    private float timeLastDeployed;
    private bool CanDeploy => timeLastDeployed + deployCooldown <= Time.unscaledTime;

    private void Start()
    {
        layoutToggleGroup = GetComponent<ToggleGroup>();
        mainCam = Camera.main;
    }
    private void Update()
    {
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D mouseRay = Physics2D.Raycast(mousePos, Vector2.zero, deployLayer);

        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (mouseRay)
            {
                if (selectedCellPrefab == null)
                {
                    Debug.Log("No Troop Selected");
                }
                else if (CanDeploy)
                {
                    DeployCell();
                }
            }
            else
            {
                Debug.Log("Please deploy on red area");
            }
        }
    }
    private void DeployCell()
    {
        GameObject cellToDeploy = Instantiate(selectedCellPrefab);
        cellToDeploy.transform.position = new Vector3(mousePos.x, mousePos.y, 0f);
        timeLastDeployed = Time.unscaledTime;
    }

    public void SearchSameCell(CellTrainingData cellData)
    {
        if(availableCells.Count == 0)
        {
            AddNewCellToList(cellData);
        }
        else
        {
            AvailableCellsData sameCell = availableCells.Find((x) => x.cellData == cellData);
            if(sameCell == null) AddNewCellToList(cellData);
            else
            {
                sameCell.amount++;
                sameCell.selectableCellUI.GetComponentInChildren<TextMeshProUGUI>().text = sameCell.amount + "x";
            }
        }
    }

    private void AddNewCellToList(CellTrainingData cellData)
    {
        GameObject newSelectableCellUI = Instantiate(selectableUIPrefab, this.transform);
        newSelectableCellUI.GetComponentsInChildren<Image>()[2].sprite = cellData.cellImage;
        newSelectableCellUI.GetComponentInChildren<TextMeshProUGUI>().text = "1x";
        Toggle toggleComponent = newSelectableCellUI.GetComponentInChildren<Toggle>();
        toggleComponent.onValueChanged.AddListener((x) => SelectCells(cellData.cellPrefab));
        toggleComponent.group = layoutToggleGroup;
        availableCells.Add(new AvailableCellsData(cellData, newSelectableCellUI, 1));
    }
    public void SelectCells(GameObject cellPrefab) => selectedCellPrefab = (selectedCellPrefab == cellPrefab) ? null : cellPrefab;
}
