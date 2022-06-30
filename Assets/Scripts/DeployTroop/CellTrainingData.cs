using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Cell Training Data", menuName = "Cell Training Data")]
public class CellTrainingData : ScriptableObject
{
    public string cellName;
    public float cost;
    public float trainTime;
    public Sprite cellImage;
    public Sprite cellUnlockedImage;
    public GameObject cellPrefab;
}
public class CellUIData
{
    public CellTrainingData cellData;
    public GameObject cellUI;
    public int amount;
    public float currentTime;

    public CellUIData(CellTrainingData cellData, GameObject cellUI, int amount)
    {
        this.cellData = cellData;
        this.cellUI = cellUI;
        this.amount = amount;
        this.currentTime = this.cellData.trainTime;
    }
}