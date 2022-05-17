using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Immune Cell", menuName = "Immune Cell")]
public class ImmuneCell : ScriptableObject
{
    public double health;
    public double atk;
    public double atkRadius;
    public float movSpd;
}
