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
    private ScrollRect trainingScrollRect;
    [SerializeField] private GameObject cellInTrainingPrefab;

    public List<CellUIData> cellsInTrainingList = new List<CellUIData>();

    private RectTransform rt;
    public float minWidth = 500f;
    public float widthToAdd = 107f;

    private BuildingManager buildingManager;
    private void Start()
    {
        buildingManager = BuildingManager.instance;
        cellSelectorScript = FindObjectOfType<CellsSelectDeploy>();
        rt = trainingLayout.GetComponent<RectTransform>();

        trainingScrollRect = trainingLayout.transform.parent.parent.GetComponent<ScrollRect>();
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
        if(GetComponentInChildren<BoneMarrow>() == null)
        {
            for(int i = 0; i < cellsInTrainingList.Count; i++)
            {
                Destroy(cellsInTrainingList[i].cellUI);
                cellsInTrainingList.RemoveAt(i);
            }
        }
    }


    public void TrainCell(CellTrainingData cellData)
    {
        if(buildingManager.resource < cellData.cost)
        {
            AudioManager.instance.PlaySound("TrainFail", SoundOutput.sfx);
            return;
        }
        else
        {
            buildingManager.resource -= cellData.cost;
            AudioManager.instance.PlaySound("TrainCell", SoundOutput.sfx);
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
        //Update and reset the scroll rect everytime a new UI instantiated
        UpdateLayourWidth(cellsInTrainingList.Count);
        trainingScrollRect.horizontalNormalizedPosition = 0;
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

    private void UpdateLayourWidth(int length)
    {
        rt.sizeDelta = new Vector2(Mathf.Max(minWidth, length * widthToAdd), rt.sizeDelta.y);
    }

    private void RemoveCellInTrainingUI(GameObject thisUI)
    {
        int index = cellsInTrainingList.FindIndex((x) => x.cellUI == thisUI);
        if(cellsInTrainingList[index].currentTime > 0)
        {
            buildingManager.resource += cellsInTrainingList[index].cellData.cost * cellsInTrainingList[index].amount;
        }
        cellsInTrainingList.RemoveAt(index);
        Destroy(thisUI);
        UpdateLayourWidth(cellsInTrainingList.Count);
    }

}
