using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private Slider progressBar;
    [SerializeField] private TextMeshProUGUI progressText;
    private TrainCells trainScript;

    private void Awake()
    {
        trainScript = BuildingManager.instance.gameObject.GetComponent<TrainCells>();
    }
    private void OnEnable()
    {
        trainScript.OnTimeUpdated += UpdateProgress;
    }
    private void OnDisable()
    {
        trainScript.OnTimeUpdated -= UpdateProgress;
    }

    private void UpdateProgress(float progress)
    {

        progressBar.value = progress;
        progressText.text = Mathf.FloorToInt(progress) + "s";
    }
}
