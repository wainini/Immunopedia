using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TrainCells : MonoBehaviour
{
    private CellsSelectDeploy cellSelectorScript;
    [SerializeField] private GameObject trainingLayout;
    [SerializeField] private GameObject cellInTrainingPrefab;

    public List<CellUIData> cellsInTrainingList = new List<CellUIData>();

    private RectTransform rt;
    public float minHeight = 400f;
    public float heightToAdd = 130f;

    private float currentCellTrainTime;
    private void Start()
    {
        cellSelectorScript = FindObjectOfType<CellsSelectDeploy>();
        rt = trainingLayout.GetComponent<RectTransform>();
    }

    private void FixedUpdate()
    {
        currentCellTrainTime -= Time.fixedDeltaTime;
        if(currentCellTrainTime <= 0 && cellsInTrainingList.Count > 0)
        {
            cellSelectorScript.AddNewCell(cellsInTrainingList[0].cellData);
            DecrementCellInTraining();
        }
    }

    public void TrainCell(CellTrainingData cellData)
    {
        int listLength = cellsInTrainingList.Count;
        if (listLength == 0)
        {
            CreateNewUI(cellData);
            currentCellTrainTime = cellData.trainTime;
        }
        else if (cellsInTrainingList[listLength - 1].cellData == cellData)
        {
            CellUIData lastTrainedCell = cellsInTrainingList[listLength - 1];
            lastTrainedCell.amount += 1;
            lastTrainedCell.cellUI.GetComponentInChildren<TextMeshProUGUI>().text = lastTrainedCell.amount + "x";
        }
        else
        {
            CreateNewUI(cellData);
        }

        UpdateLayoutHeight(cellsInTrainingList.Count);
    }

    private void CreateNewUI(CellTrainingData cellData)
    {
        GameObject cellInTraining = Instantiate(cellInTrainingPrefab, trainingLayout.transform);
        cellInTraining.GetComponentsInChildren<Image>()[1].sprite = cellData.cellImage;
        cellInTraining.GetComponentInChildren<TextMeshProUGUI>().text = "1x";
        cellInTraining.GetComponentInChildren<Button>().onClick.AddListener(() => RemoveCellInTrainingUI(cellInTraining));
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
        if(index == 0 && cellsInTrainingList.Count > 0)
        {
            currentCellTrainTime = cellsInTrainingList[0].cellData.trainTime;
        }
        Destroy(thisUI);
    }

    private void DecrementCellInTraining()
    {
        CellUIData trainedCell = cellsInTrainingList[0];
        trainedCell.amount--;
        if (trainedCell.amount > 0)
        {
            currentCellTrainTime = trainedCell.cellData.trainTime;
            trainedCell.cellUI.GetComponentInChildren<TextMeshProUGUI>().text = trainedCell.amount + "x";
        }
        else
        {
            RemoveCellInTrainingUI(trainedCell.cellUI);
        }
    }

}
