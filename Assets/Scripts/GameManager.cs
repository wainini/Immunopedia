using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    
    [SerializeField] private int lives;
    [HideInInspector] public List<GameObject> wounds = new List<GameObject>();

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
        Debug.Log("You have " + lives + " lives");
    }

    public void ReduceLive()
    {
        lives--;
        if (lives <= 0) GameOver();
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
    }

    private void Win()
    {
        //win
        Debug.Log("You Win");
        Time.timeScale = 0;
    }
}
