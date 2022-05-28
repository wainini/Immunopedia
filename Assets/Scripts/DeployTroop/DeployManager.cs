using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DeployManager : MonoBehaviour
{
    public static event Func<int> WhatTroop;
    public delegate GameObject GetTroop(int index);
    public static GetTroop OnDeployTroop;

    [SerializeField] private LayerMask deployLayer;

    private Camera mainCam;
    private Vector3 mousePos;
    private int troopToDeploy;

    [Header("Deploy Cooldown")]
    [SerializeField] private float deployCooldown = 0.5f;
    private float timeLastDeployed;
    private bool CanDeploy => timeLastDeployed + deployCooldown <= Time.unscaledTime;


    private void Start()
    {
        mainCam = Camera.main;
    }
    private void Update()
    {
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D mouseRay = Physics2D.Raycast(mousePos, Vector2.zero, deployLayer);

        if (Input.GetKey(KeyCode.Mouse0))
        {
            if(mouseRay)
            {
                troopToDeploy = (int)WhatTroop?.Invoke();
                if(troopToDeploy == 69)
                {
                    Debug.Log("No Troop Selected");
                }
                else if(CanDeploy)
                {
                    GetAndDeployTroop();
                }
            }
            else
            {
                Debug.Log("Please deploy on red area");
            }
        }
    }

    private void GetAndDeployTroop()
    {
        GameObject troop = OnDeployTroop?.Invoke(troopToDeploy);
        troop.SetActive(true);
        troop.transform.position = new Vector3(mousePos.x, mousePos.y, 0f);
        timeLastDeployed = Time.unscaledTime;
    }
}
