using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Alamanac Cell Info", menuName = "Almanac Cell Info")]
public class AlmanacCellInfo : ScriptableObject
{
    public string cellName;
    public Sprite cellButtonImage;
    public string cellDesc;
    public GameObject cellPrefab;
    public bool isEnemy;
}
