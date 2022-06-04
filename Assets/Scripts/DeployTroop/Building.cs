using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Building : MonoBehaviour
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
