using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Upgrade Data", menuName = "Upgrade Data")]
public class UpgradeData : ScriptableObject
{
    public string upgradeTitle;
    public string upgradeDescription;
    public int upgradeCost;
}
