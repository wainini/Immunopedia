using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    private int availableScore;
    private void Start()
    {
        if(!PlayerPrefs.HasKey(SceneManager.GetActiveScene().name))
        {
            PlayerPrefs.SetInt(SceneManager.GetActiveScene().name, 0);
        }
        availableScore = PlayerPrefs.GetInt(SceneManager.GetActiveScene().name);
    }

    public int GetScore(int currentScore)
    {
        if (currentScore > availableScore)
        {
            PlayerPrefs.SetInt(SceneManager.GetActiveScene().name, currentScore);
        }
        return currentScore;
    }
}
