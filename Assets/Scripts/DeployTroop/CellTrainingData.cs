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
    public GameObject troopPrefab;
}
