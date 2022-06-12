using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BuildingPopUp : MonoBehaviour
{
    [SerializeField] private GameObject popUp;
    public void ViewPopUp()
    {
        popUp.SetActive(true);
    }

    public void ExitPopUp()
    {
        popUp.SetActive(false);
    }

}