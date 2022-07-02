using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class CreateTroopButtons : MonoBehaviour
{
    [SerializeField] private GameObject buttonsLayout;
    [SerializeField] private TrainCells trainCellScript;
    private List<CellTrainingData> cellDatas;
    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private GameObject unlockedPrefab;

    private void Awake()
    {
        if (SceneManager.GetActiveScene().name == "Level 1")
        {
            PlayerPrefs.SetInt("Neutrofil", 1);
            PlayerPrefs.SetInt("Platelet", 1);
            PlayerPrefs.SetInt("Bacteria", 1);
        }
        if (SceneManager.GetActiveScene().name == "Level 5")
        {
            PlayerPrefs.SetInt("Eosinophil", 1);
            PlayerPrefs.SetInt("Parasite", 1);
        }
        if (SceneManager.GetActiveScene().name == "Level 9")
        {
            PlayerPrefs.SetInt("Macrophage", 1);
        }

        cellDatas = BuildingManager.instance.cellDatas;
        foreach(CellTrainingData cellData in cellDatas)
        {
            if(PlayerPrefs.GetInt(cellData.name, 0) == 1)
            {
                GameObject button = Instantiate(buttonPrefab, buttonsLayout.transform);
                button.GetComponentsInChildren<Image>()[1].sprite = cellData.cellImage;
                button.GetComponentInChildren<TextMeshProUGUI>().text = cellData.cost.ToString();
                button.name = cellData.cellName;
                button.GetComponent<Button>().onClick.AddListener(() => trainCellScript.TrainCell(cellData));
            }
            else
            {
                GameObject unlockedImage = Instantiate(unlockedPrefab, buttonsLayout.transform);
                unlockedImage.GetComponent<Image>().sprite = cellData.cellUnlockedImage;
            }
        }
    }
}
