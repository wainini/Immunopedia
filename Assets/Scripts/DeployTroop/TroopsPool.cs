using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroopsPool : MonoBehaviour
{
    [SerializeField] private List<GameObject> availableTroops;

    private int selectedTroop = 69;

    private Queue<GameObject>[] troopsPools = new Queue<GameObject>[3];
    private void OnEnable()
    {
        DeployManager.WhatTroop += ReturnTroopIndex;
        DeployManager.OnDeployTroop += GetTroopFromPool;
    }

    private void OnDisable()
    {
        DeployManager.WhatTroop -= ReturnTroopIndex;
        DeployManager.OnDeployTroop -= GetTroopFromPool;
    }

    private void Start()
    { 

        for (int i = 0; i < availableTroops.Count; i++)
        {
            troopsPools[i] = new Queue<GameObject>();
            for (int j = 0; j < 5; j++)
            {
                AddTroopToPool(availableTroops[i], troopsPools[i]);
            }
        }
    }

    private void AddTroopToPool(GameObject troop, Queue<GameObject> queue)
    {
        GameObject temp = Instantiate(troop, this.transform);
        temp.SetActive(false);
        queue.Enqueue(temp);
    }

    private GameObject GetTroopFromPool(int index)
    {
        if(troopsPools[index].Count == 0)
        {
            AddTroopToPool(availableTroops[index], troopsPools[index]);
        }
        return troopsPools[index].Dequeue();
    }

    private void ReturnTroopToPool(GameObject troop, int index)
    {
        troopsPools[index].Enqueue(troop);
        troop.SetActive(false);
    }

    public void SelectTroop(int index)
    {
        selectedTroop = (selectedTroop == index) ? 69 : index;
    }

    private int ReturnTroopIndex()
    {
        if (selectedTroop == 69)
        {
            return 69;
        }
        return selectedTroop;
    }
}
