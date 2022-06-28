using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSpeed : MonoBehaviour
{
    public static GameSpeed instance;
    public float currentGameSpeed;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    private void Start()
    {
        currentGameSpeed = 1f;
        Time.timeScale = 1f;
    }
    private void OnDisable()
    {
        Time.timeScale = 1f;
    }
    public void SetGameSpeed(float value)
    {
        Time.timeScale = value;
        currentGameSpeed = value;
    }
}
