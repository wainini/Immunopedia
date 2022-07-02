using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Almanac : MonoBehaviour
{
    private BuildingManager buildingManager;
    [SerializeField] private Sprite enemyBackground;
    [SerializeField] private GameObject cellInfoPanel;
    [SerializeField] private GameObject buttonLayout;
    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private GameObject lockedPrefab;
    [SerializeField] private GameObject comingSoonPrefab;
    [SerializeField] private List<AlmanacCellInfo> almanacCellInfos;
    [SerializeField] private List<AlmanacCellInfo> almanacEnemyInfos;
    [SerializeField] private Transform cellAnimationPos;
    // Start is called before the first frame update
    void Start()
    {
        //PlayerPrefs.SetInt("Neutrofil", 1);
        //PlayerPrefs.SetInt("Platelet", 1);
        //PlayerPrefs.SetInt("Bacteria", 1);
        //PlayerPrefs.SetInt("Eosinophil", 0);
        //PlayerPrefs.SetInt("Parasite", 0);
        //PlayerPrefs.SetInt("Macrophage", 0);
        foreach (AlmanacCellInfo cellInfo in almanacCellInfos)
        {
            if (PlayerPrefs.GetInt(cellInfo.cellName, 0) == 1)
            {
                GameObject button = Instantiate(buttonPrefab, buttonLayout.transform);
                button.GetComponentsInChildren<Image>()[1].sprite = cellInfo.cellButtonImage;
                //button.GetComponentInChildren<TextMeshProUGUI>().text = cellInfo.cellName;
                button.name = cellInfo.cellName;
                button.GetComponent<Button>().onClick.AddListener(() => OnButtonClick(cellInfo));
            }
            else
            {
                GameObject button = Instantiate(lockedPrefab, buttonLayout.transform);
                button.GetComponentsInChildren<Image>()[1].sprite = cellInfo.cellButtonImage;
            }
        }
        Instantiate(comingSoonPrefab, buttonLayout.transform);
        Instantiate(comingSoonPrefab, buttonLayout.transform);

        foreach (AlmanacCellInfo cellInfo in almanacEnemyInfos)
        {
            if (PlayerPrefs.GetInt(cellInfo.cellName, 0) == 1)
            {
                GameObject button = Instantiate(buttonPrefab, buttonLayout.transform);
                button.GetComponentsInChildren<Image>()[1].sprite = cellInfo.cellButtonImage;
                //button.GetComponentInChildren<TextMeshProUGUI>().text = cellInfo.cellName;
                button.GetComponent<Image>().sprite = enemyBackground;
                button.name = cellInfo.cellName;
                button.GetComponent<Button>().onClick.AddListener(() => OnButtonClick(cellInfo));
            }
            else
            {
                GameObject button = Instantiate(lockedPrefab, buttonLayout.transform);
                button.GetComponentsInChildren<Image>()[1].sprite = cellInfo.cellButtonImage;
            }
        }
        Instantiate(comingSoonPrefab, buttonLayout.transform);
    }

    public void OnButtonClick(AlmanacCellInfo cellInfo)
    {
        GameObject cellAnimation = Instantiate(cellInfo.cellPrefab, cellInfoPanel.transform);
        cellAnimation.transform.position = cellAnimationPos.transform.position;
        Destroy(cellAnimationPos.gameObject);
        cellAnimationPos = cellAnimation.transform;
        //cellInfoPanel.GetComponentsInChildren<Image>()[1].sprite = cellInfo.cellInfoImage;
        cellInfoPanel.GetComponentInChildren<TextMeshProUGUI>().text = cellInfo.cellDesc;
        cellInfoPanel.GetComponentInChildren<ScrollRect>().verticalNormalizedPosition = 1;
    }
}
