using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Almanac : MonoBehaviour
{
    private BuildingManager buildingManager;
    [SerializeField] private GameObject cellInfoPanel;
    [SerializeField] private GameObject buttonLayout;
    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private List<AlmanacCellInfo> almanacCellInfos;
    // Start is called before the first frame update
    void Start()
    {
        buildingManager = BuildingManager.instance;
        foreach (AlmanacCellInfo cellInfo in almanacCellInfos)
        {
            GameObject button = Instantiate(buttonPrefab, buttonLayout.transform);
            button.GetComponentsInChildren<Image>()[1].sprite = cellInfo.cellButtonImage;
            button.GetComponentInChildren<TextMeshProUGUI>().text = cellInfo.cellName;
            button.name = cellInfo.cellName;
            button.GetComponent<Button>().onClick.AddListener(() => OnButtonClick(cellInfo));
        }
    }

    public void OnButtonClick(AlmanacCellInfo cellInfo)
    {
        cellInfoPanel.GetComponentsInChildren<Image>()[1].sprite = cellInfo.cellInfoImage;
        cellInfoPanel.GetComponentInChildren<TextMeshProUGUI>().text = cellInfo.cellDesc;
    }
}
