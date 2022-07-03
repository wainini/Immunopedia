using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    public event Action<int> OnReduceLive;

    [SerializeField] private int maxLives;
    [HideInInspector] public List<GameObject> wounds;

    private int currentLives;
    private int score;
    private int totalScores;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        totalScores = PlayerPrefs.GetInt("Total Stars", 0);
    }

    private void Start()
    {
        var woundArr = GameObject.FindGameObjectsWithTag("Wound");
        wounds = new List<GameObject>(woundArr);
        currentLives = maxLives;
        OnReduceLive?.Invoke(currentLives);
    }

    private void Update()
    {
        CheckWinCondition();
    }

    public void ReduceLive(int reductionAmount)
    {
        currentLives -= reductionAmount;
        if (currentLives <= 0) GameOver();
        OnReduceLive?.Invoke(currentLives);
    }

    public void CloseWound(GameObject wound)
    {
        wounds.Remove(wound);
    }

    private void CheckWinCondition()
    {
        /*
         * kondisi untuk menang:
         * - semua wound udah tertutup
         * - semua musuh udah mati
         * - kalau HP Seal nyentuh 0 sebelum semua kondisi diatas terpenuhi, gameover
         */

        //kondisi pertama tercapai
        if (wounds.Count == 0)
        {
            //cari semua GameObject enemy
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

            //kalau enemies bernilai 0, berarti udah menang
            if (enemies.Length == 0) Win();
        }
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
