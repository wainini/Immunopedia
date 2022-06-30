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
        if (gameSpeedScript != null)
            gameSpeedScript.SetGameSpeed(gameSpeedScript.currentGameSpeed);
        else
            GameSpeed.instance.SetGameSpeed(GameSpeed.instance.currentGameSpeed);
    }
}
