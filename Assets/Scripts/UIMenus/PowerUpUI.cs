using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpUI : MonoBehaviour
{
    public Text totalStars;
    public int sceneCount;

    
    void Start()
    {
        int total = 0;
        for(int i = 1; i <= sceneCount; i++)
        {
            total += PlayerPrefs.GetInt("Level " + i);
        }
        totalStars.text = total.ToString();
    }
}
