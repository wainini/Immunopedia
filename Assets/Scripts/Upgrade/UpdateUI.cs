using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateUI : MonoBehaviour
{
    public void RefreshUI()
    {
        PowerUpUI ui = gameObject.GetComponent<PowerUpUI>();
        // Update StarQty
        ui.UpdateStarsQty();
        // Update Skill Button Interactability
        ui.EnableButtons();
    }
}
