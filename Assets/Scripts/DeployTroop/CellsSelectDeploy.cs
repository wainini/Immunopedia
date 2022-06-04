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

    private Camera mainCamera;

    [Header("Deploy")]
    [SerializeField] private LayerMask deployLayer;
    [SerializeField] private float deployCooldown = 0.5f;
    private float lastDeployed;
    private bool CanDeploy => lastDeployed + deployCooldown <= Time.time;
    private void Start()
    {
        selectorLayoutToggleGroup = selectorLayout.GetComponent<ToggleGroup>();
        mainCamera = Camera.main;
    }

    private void Update()
    {

    }

    public void AddNewCell(CellTrainingData cellData)
    {
        if(availableCells.Count == 0)
        {
            CreateNewCell(cellData);
        }
        else
        {
            CellUIData sameCell = availableCells.Find((x) => x.cellData == cellData);
            if (sameCell == null) CreateNewCell(cellData);
            else
            {
                sameCell.amount++;
                sameCell.cellUI.GetComponentInChildren<TextMeshProUGUI>().text = sameCell.amount + "x";
            }
        }
    }

    private void CreateNewCell(CellTrainingData cellData)
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
