using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class TrainCells : MonoBehaviour
{
    public event Action<float> OnTimeUpdated;

    private CellsSelectDeploy cellSelectorScript;
    [SerializeField] private GameObject trainingLayout;
    [SerializeField] private GameObject cellInTrainingPrefab;

    public List<CellUIData> cellsInTrainingList = new List<CellUIData>();

    private RectTransform rt;
    public float minHeight = 250f;
    public float heightToAdd = 130f;

    private BuildingManager buildingManager;
    private void Start()
    {
        buildingManager = BuildingManager.instance;
        cellSelectorScript = FindObjectOfType<CellsSelectDeploy>();
        rt = trainingLayout.GetComponent<RectTransform>();

        if(trainingLayout == null) { 
        }
    }

    private void FixedUpdate()
    {
        OnTimeUpdated?.Invoke(1);
        for (int i = 0; i<cellsInTrainingList.Count; i++)
        {
            CellUIData cell = cellsInTrainingList[i];
            cell.currentTime -= Time.fixedDeltaTime;

            Slider progressBar = cell.cellUI.GetComponentInChildren<Slider>();
            progressBar.value = cell.cellData.trainTime - cell.currentTime;
            progressBar.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = Mathf.FloorToInt(cell.currentTime) + "s";
            if (cell.currentTime <= 0)
            {
                cellSelectorScript.AddNewCell(cell.cellData, cell.amount);
                RemoveCellInTrainingUI(cell.cellUI);
            }
        }
    }


    public void TrainCell(CellTrainingData cellData)
    {
        if(buildingManager.resource < cellData.cost)
        {
            Debug.Log("You don't have enough resources");
            return;
        }
        else
        {
            buildingManager.resource -= cellData.cost;
        }
        int listLength = cellsInTrainingList.Count;
        CellUIData sameCell = null;
        if (listLength == 0)
        {
            CreateNewUI(cellData);
        }
        else
        {
            foreach (CellUIData cell in cellsInTrainingList)
            {
                if (cell.cellData == cellData && cell.currentTime == cellData.trainTime)
                {

                    sameCell = cell;
                    break;
                }
            }

            if(sameCell != null)
            {
                sameCell.amount += 1;
                sameCell.cellUI.GetComponentInChildren<TextMeshProUGUI>().text = sameCell.amount + "x";
            }
            else
            {
                CreateNewUI(cellData);
            }
        }

        UpdateLayoutHeight(cellsInTrainingList.Count);
    }

    private void CreateNewUI(CellTrainingData cellData)
    {
        GameObject cellInTraining = Instantiate(cellInTrainingPrefab, trainingLayout.transform);
        cellInTraining.GetComponentsInChildren<Image>()[1].sprite = cellData.cellImage;
        cellInTraining.GetComponentInChildren<TextMeshProUGUI>().text = "1x";
        cellInTraining.GetComponentInChildren<Button>().onClick.AddListener(() => RemoveCellInTrainingUI(cellInTraining));
        Slider progressBar = cellInTraining.GetComponentInChildren<Slider>();
        progressBar.maxValue = cellData.trainTime;
        progressBar.minValue = 0;
        progressBar.value = 0;
        progressBar.GetComponentInChildren<TextMeshProUGUI>().text = cellData.trainTime + "s";
        cellsInTrainingList.Add(new CellUIData(cellData, cellInTraining, 1));
    }

    private void UpdateLayoutHeight(int length)
    {
        int howManyToAdd = (length % 2 == 1) ? ((length - 6) / 2 + 1) : ((length - 6) / 2);
        rt.sizeDelta = new Vector2(rt.sizeDelta.x, Mathf.Max(minHeight, minHeight + howManyToAdd * heightToAdd));
    }

    private void RemoveCellInTrainingUI(GameObject thisUI)
    {
        int index = cellsInTrainingList.FindIndex((x) => x.cellUI == thisUI);
        cellsInTrainingList.RemoveAt(index);
        Destroy(thisUI);
    }

}
