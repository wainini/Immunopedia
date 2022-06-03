using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CreateTroopButtons : MonoBehaviour
{
    [SerializeField] private GameObject buttonsLayout;
    [SerializeField] private TrainCells trainCellScript;
    [SerializeField] private List<CellTrainingData> cellDatas;
    [SerializeField] private GameObject buttonPrefab;

    private void Awake()
    {
        foreach(CellTrainingData cellData in cellDatas)
        {
            GameObject button = Instantiate(buttonPrefab, buttonsLayout.transform);
            button.GetComponentsInChildren<Image>()[1].sprite = cellData.cellImage;
            button.GetComponentInChildren<TextMeshProUGUI>().text = "Cost: " + cellData.cost;
            button.name = cellData.cellName;
            button.GetComponent<Button>().onClick.AddListener(() => trainCellScript.TrainCell(cellData));
        }
    }

}
