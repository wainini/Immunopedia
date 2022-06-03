using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TrainCells : MonoBehaviour
{
    [SerializeField] private GameObject cellInTrainingPrefab;
    class InTrainingData
    {
        public CellTrainingData cellData;
        public GameObject inTrainingUI;
        public int amount;

        public InTrainingData(CellTrainingData cellData, GameObject inTrainingUI, int amount)
        {
            this.cellData = cellData;
            this.inTrainingUI = inTrainingUI;
            this.amount = amount;
        }
    }

    List<InTrainingData> cellsInTrainingList = new List<InTrainingData>();

    private RectTransform rt;
    public float minHeight = 400f;
    public float heightToAdd = 130f;

    private void Start()
    {
        rt = GetComponent<RectTransform>();
    }

    public void TrainCell(CellTrainingData cellData)
    {
        int listLength = cellsInTrainingList.Count;
        if (listLength == 0)
        {
            CreateNewUI(cellData);
        }
        else if (cellsInTrainingList[listLength - 1].cellData == cellData)
        {
            InTrainingData lastTrainedCell = cellsInTrainingList[listLength - 1];
            lastTrainedCell.amount += 1;
            lastTrainedCell.inTrainingUI.GetComponentInChildren<TextMeshProUGUI>().text = lastTrainedCell.amount + "x";
        }
        else
        {
            CreateNewUI(cellData);
        }

        UpdateLayoutHeight(cellsInTrainingList.Count);
    }

    private void CreateNewUI(CellTrainingData cellData)
    {
        GameObject cellInTraining = Instantiate(cellInTrainingPrefab, this.transform);
        cellInTraining.GetComponentsInChildren<Image>()[1].sprite = cellData.cellImage;
        cellInTraining.GetComponentInChildren<TextMeshProUGUI>().text = "1x";
        cellInTraining.GetComponentInChildren<Button>().onClick.AddListener(() => RemoveTroopInTraining(cellInTraining));
        cellsInTrainingList.Add(new InTrainingData(cellData, cellInTraining, 1));
    }

    private void UpdateLayoutHeight(int length)
    {
        int howManyToAdd = (length % 2 == 1) ? ((length - 6) / 2 + 1) : ((length - 6) / 2);
        rt.sizeDelta = new Vector2(rt.sizeDelta.x, Mathf.Max(minHeight, minHeight + howManyToAdd * heightToAdd));
    }

    private void RemoveTroopInTraining(GameObject thisUI)
    {
        int index = cellsInTrainingList.FindIndex((x) => x.inTrainingUI == thisUI);
        cellsInTrainingList.RemoveAt(index);
        Destroy(thisUI);
    }
}
