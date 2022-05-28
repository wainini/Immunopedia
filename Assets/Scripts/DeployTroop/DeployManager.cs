using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DeployManager : MonoBehaviour
{
    [SerializeField] private LayerMask deployLayer;
    private Camera mainCam;

    public static event Func<int> WhatTroop;
    public delegate GameObject GetTroop(int index);
    public static GetTroop SpawnTroop;

    private int troopToDeploy;

    private void Start()
    {
        mainCam = Camera.main;
    }
    private void Update()
    {
        Vector3 mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D mouseRay = Physics2D.Raycast(mousePos, Vector2.zero, deployLayer);

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if(mouseRay)
            {
                troopToDeploy = (int)WhatTroop?.Invoke();
                Debug.Log(troopToDeploy);
                if(troopToDeploy == 69)
                {
                    Debug.Log("No Troop Selected");
                }
                else
                {
                    GameObject troop = SpawnTroop?.Invoke(troopToDeploy);
                    troop.SetActive(true);
                    troop.transform.position = new Vector3(mousePos.x, mousePos.y, 0f);
                }
            }
            else
            {
                Debug.Log("Please deploy on red area");
            }
        }
    }
}
