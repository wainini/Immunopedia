using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    
    [SerializeField] private int maxLives;
    [HideInInspector] public Queue<GameObject> wounds;

    private int currentLives;
    private int score;
    private int totalScores;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        if(!PlayerPrefs.HasKey("Total Stars"))
        {
            PlayerPrefs.SetInt("Total Stars", 0);
        }
        totalScores = PlayerPrefs.GetInt("Total Stars");
    }

    private void Start()
    {
        var woundArr = GameObject.FindGameObjectsWithTag("Wound");
        wounds = new Queue<GameObject>(woundArr);
        currentLives = maxLives;
    }

    public void ReduceLive(int reductionAmount)
    {
        currentLives -= reductionAmount;
        if (currentLives <= 0) GameOver();
    }

    public void CloseWound(GameObject wound)
    {
        wounds.Dequeue();
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
        Debug.Log("You Won");
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
        score = GetComponent<ScoreManager>().GetScore(score);
        totalScores += score;
        PlayerPrefs.SetInt("Total Stars", totalScores);
    }
}
