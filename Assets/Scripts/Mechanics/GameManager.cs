using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    
    [SerializeField] private int maxLives;
    [HideInInspector] public List<GameObject> wounds = new List<GameObject>();

    private int currentLives;
    private int score;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        var woundArr = GameObject.FindGameObjectsWithTag("Wound");
        wounds = new List<GameObject>(woundArr);
        currentLives = maxLives;
    }

    public void ReduceLive(int reductionAmount)
    {
        currentLives -= reductionAmount;
        if (currentLives <= 0) GameOver();
    }

    public void CloseWound(GameObject wound)
    {
        wounds.Remove(wound);
        if (wounds.Count == 0) Win();
    }

    private void GameOver()
    {
        //game over
        Debug.Log("You Died");
        Time.timeScale = 0;
        score = 0;
    }

    private void Win()
    {
        //win
        Time.timeScale = 0;
        if(currentLives == maxLives)
        {
            score = 3;
        }else if ((float)currentLives / (float)maxLives >= 0.5f)
        {
            score = 2;
        }else if((float)currentLives / (float)maxLives < 0.5f)
        {
            score = 1;
        }
    }
}
