using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroopsSelector : MonoBehaviour
{
    [SerializeField] private List<GameObject> availableTroops;

    private int selectedTroop = 69;

    private void OnEnable()
    {
        DeployManager.WhatTroop += ReturnTroop;
    }

    private void OnDisable()
    {
        DeployManager.WhatTroop -= ReturnTroop;
    }

    public void SelectTroop(int index)
    {
        selectedTroop = (selectedTroop == index) ? 69 : index;
    }

    private GameObject ReturnTroop()
    {
        if(selectedTroop == 69)
        {
            return null;
        }
        return availableTroops[selectedTroop];
    }
}
