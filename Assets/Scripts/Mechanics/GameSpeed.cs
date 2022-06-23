using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSpeed : MonoBehaviour
{

    private void Start()
    {
        Time.timeScale = 1f;
    }
    private void OnDisable()
    {
        Time.timeScale = 1f;
    }
    public void SetGameSpeed(float value)
    {
        Time.timeScale = value;
    }
}
