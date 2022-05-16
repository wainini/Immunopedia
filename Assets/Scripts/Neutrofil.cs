using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Neutrofil : MonoBehaviour
{
    public ImmuneCell immuneCell;
    public double health;
    public double atk;

    private void Start()
    {
        Debug.Log(this.immuneCell.health + " " + this.immuneCell.atk);
        health = immuneCell.health;
        atk = immuneCell.atk;
    }
}
