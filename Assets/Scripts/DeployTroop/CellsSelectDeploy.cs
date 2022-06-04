using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CellsSelectDeploy : MonoBehaviour
{
    List<CellUIData> availableCells = new List<CellUIData>();
    [SerializeField] private GameObject selectorLayout;
    [SerializeField] private GameObject selectorPrefab;
    private ToggleGroup selectorLayoutToggleGroup;

    private GameObject selectedCell;

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
        selectorLayoutToggleGroup = selectorLayout.GetComponent<ToggleGroup>();
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

    public void AddNewCell(CellTrainingData cellData)
    {
        if(availableCells.Count == 0)
        {
            AddNewCellToList(cellData);
        }
        else
        {
            CellUIData sameCell = availableCells.Find((x) => x.cellData == cellData);
            if (sameCell == null) AddNewCellToList(cellData);
            else
            {
                sameCell.amount++;
                sameCell.cellUI.GetComponentInChildren<TextMeshProUGUI>().text = sameCell.amount + "x";
            }
        }
    }

    private void AddNewCellToList(CellTrainingData cellData)
    {
        GameObject newSelector = Instantiate(selectorPrefab, selectorLayout.transform);
        newSelector.GetComponentsInChildren<Image>()[2].sprite = cellData.cellImage;
        newSelector.GetComponentInChildren<TextMeshProUGUI>().text = "1x";
        Toggle toggleComponent = newSelector.GetComponent<Toggle>();
        toggleComponent.onValueChanged.AddListener((x) => SelectCell(cellData.cellPrefab));
        toggleComponent.group = selectorLayoutToggleGroup;
        availableCells.Add(new CellUIData(cellData, newSelector, 1));
    }

    private void SelectCell(GameObject cellPrefab) => selectedCell = (selectedCell == cellPrefab) ? null : cellPrefab;
}
