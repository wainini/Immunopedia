using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseWhenOpen : MonoBehaviour
{
    GameSpeed gameSpeedScript;
    private void Awake()
    {
        gameSpeedScript = GameSpeed.instance;
    }
    private void OnEnable()
    {
        Time.timeScale = 0f;
    }

    private void OnDisable()
    {
        gameSpeedScript.SetGameSpeed(gameSpeedScript.currentGameSpeed);
    }
}
