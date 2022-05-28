using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DeployManager : MonoBehaviour
{
    [SerializeField] private LayerMask deployLayer;
    private Camera mainCam;

    public static Func<GameObject> WhatTroop;

    private GameObject troopToDeploy;

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
                troopToDeploy = WhatTroop?.Invoke();
                if(troopToDeploy == null)
                {
                    Debug.Log("No Troop Selected");
                }
                else
                {
                    Instantiate(troopToDeploy, new Vector3(mousePos.x, mousePos.y, 0f), Quaternion.identity);
                    Debug.Log(troopToDeploy.name);
                }
            }
            else
            {
                Debug.Log("Please deploy on red area");
            }
        }
    }
}
